namespace Codefarts.UIControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Bind class for creating bindings between properties
    /// using expressions.
    /// </summary>
    /// <remarks>
    /// Based on https://github.com/praeclarum/Bind. 
    /// Based on https://github.com/Muraad/Bind.
    /// Heavily rewritten and simplified.
    /// </remarks>
    public static class Bind
    {
        internal class ExpressionData
        {
            private MemberInfo member;

            public ExpressionData()
            {
            }

            public ExpressionData(Expression expression, MemberInfo member)
            {
                this.Expression = expression;
                this.member = member;
            }

            public Expression Expression { get; set; }
            public MemberInfo Info { get; set; }
        }

        /// <summary>
        /// Create a binding from given expression
        /// </summary>
        /// <typeparam name="T">
        /// The binding expression result type (Is always bool)
        /// </typeparam>
        /// <param name="bindingExpression">
        /// The binding expression
        /// </param>
        /// <returns>
        /// An IDisposable that can be used to remove the binding when Dispose() is called
        /// </returns>
        public static IDisposable Create<T>(Expression<Func<T>> bindingExpression)
        {
            var bindings = DisposableBindingsFromAndAlsoExpressions(GetAndAlsoExpressions(bindingExpression.Body));
            return Disposable.CreateContainer(bindings.ToArray());
        }

        #region Private

        #region Break down Expression into AndAlso List<Expression> and then create bindings

        /// <summary>
        /// The get and also expressions.
        /// </summary>
        /// <param name="expr">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        static List<Expression> GetAndAlsoExpressions(Expression expr)
        {
            var parts = new List<Expression>();

            if (expr.NodeType == ExpressionType.AndAlso)
            {
                var b = (BinaryExpression)expr;

                SplitAndAlsoExpressions(parts, b);

                // The parse process was from right (end) to left (start).
                // So reverse the expression list.
                parts.Reverse();

                parts.Select(expression => GetAndAlsoExpressions(expression)).ToArray().ForEach(parts.AddRange);
            }

            if (expr.NodeType == ExpressionType.Equal)
            {
                var b = (BinaryExpression)expr;
                parts.Add(expr);
            }

            return parts;
        }

        /// <summary>
        /// The split and also expressions.
        /// </summary>
        /// <param name="parts">
        /// The parts.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        static void SplitAndAlsoExpressions(List<Expression> parts, BinaryExpression b)
        {
            // split all expressions
            while (b != null)
            {
                // get left part of the binary expression.
                var l = b.Left;

                // the right part have to be also non complex type! See readme at github.
                // So just add it to the expression part list.
                parts.Add(b.Right);

                // If this is again no "==" expression, then get the next binary expr
                // and start again.
                if (l.NodeType == ExpressionType.AndAlso)
                    b = (BinaryExpression)l;
                else
                {
                    // horray, the end is reached, we have the last "==" expression.
                    parts.Add(l);
                    b = null;
                }
            }
        }

        /// <summary>
        /// Creates a binding (IDisposable) for every given seperated Expression that is of type ExpressionType.Equal
        /// </summary>
        /// <remarks>
        /// Takes results from GetAndAlsoExpressions(..)
        /// </remarks>
        /// <param name="parts">
        /// The given expressions.
        /// </param>
        /// <returns>
        /// The a list of IDisposables, one for every created binding, that can be used for unbinding.
        /// </returns>
        static List<IDisposable> DisposableBindingsFromAndAlsoExpressions(IEnumerable<Expression> parts)
        {
            var result = new List<IDisposable>();
            foreach (var part in parts)
            {
                if (part.NodeType == ExpressionType.Equal)
                {
                    var b = (BinaryExpression)part;
                    result.Add(CreateBinding(b.Left, b.Right));
                }
            }

            return result;
        }

        #endregion

        #region Create IDisposable binding (simple or complex) from left and right expression

        /// <summary>
        /// Creates a binding from given left and right expression parts.
        /// </summary>
        /// <remarks>
        /// Parts are from an ExpressionType.Equal.
        /// Left expression have to be an ExpressionType.MemberAccess!
        /// But thats not that hard. Compiler/TypeChecker is taking care this cannot happen.
        /// </remarks>
        /// <param name="left">
        /// The left expression. Note type have to be ExpressionType.MemberAccess
        /// </param>
        /// <param name="right">
        /// The right (complex) expression
        /// </param>
        /// <returns>
        /// An IDisposable representing the created binding. Dispose for unbinding. Null if binding not created.
        /// </returns>
        static IDisposable CreateBinding(Expression left, Expression right)
        {
            if (left.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("NOT ALLOWED");
            }

            IDisposable result = null;
            if (right.NodeType == ExpressionType.MemberAccess)   // right is simple too
            {
                result = CreateSimpleBinding(left, right);
            }
            else
            {
                result = CreateComplex(left, right);
            }

            return result;
        }

        #region Create simple binding

        /// <summary>
        /// The create simple binding.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        static IDisposable CreateSimpleBinding(Expression left, Expression right)
        {
            var leftB = BindingMember.FromExpression(left as MemberExpression);
            var rightB = BindingMember.FromExpression(right as MemberExpression);

            var disposable = new DisposableContainer();

            // left GetMethod is available and right set method too
            // -> when left property changes then set right property
            CreateSimpleLeftToRight(leftB, rightB, disposable);
            CreateSimpleRightToLeft(leftB, rightB, disposable);
            return disposable;
        }

        /// <summary>
        /// The create simple right to left.
        /// </summary>
        /// <param name="leftB">
        /// The left b.
        /// </param>
        /// <param name="rightB">
        /// The right b.
        /// </param>
        /// <param name="disposable">
        /// The disposable.
        /// </param>
        private static void CreateSimpleRightToLeft(BindingMember leftB, BindingMember rightB, DisposableContainer disposable)
        {
            if (leftB.SetMethod != null && rightB.GetMethod != null)
            {
                disposable.AddDisposable(AddChangeNotificationEventHandler(rightB.Target, rightB.PropertyName, () =>
                {
                    leftB.SetMethod(rightB.GetMethod());
                }));
            }
        }

        /// <summary>
        /// The create simple left to right.
        /// </summary>
        /// <param name="leftB">
        /// The left b.
        /// </param>
        /// <param name="rightB">
        /// The right b.
        /// </param>
        /// <param name="disposable">
        /// The disposable.
        /// </param>
        private static void CreateSimpleLeftToRight(BindingMember leftB, BindingMember rightB, DisposableContainer disposable)
        {
            if (leftB.GetMethod != null && rightB.SetMethod != null)
            {
                disposable.AddDisposable(AddChangeNotificationEventHandler(leftB.Target, leftB.PropertyName, () =>
                {
                    rightB.SetMethod(leftB.GetMethod());
                }));
            }
        }

        #endregion

        #region Create complex binding

        /// <summary>
        /// The create complex.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        static IDisposable CreateComplex(Expression left, Expression right)
        {
            var disposable = new DisposableContainer();
            var leftMemEx = left as MemberExpression;

            // Get the instance the property belongs too
            var leftTarget = EvalExpression(leftMemEx.Expression);

            // Get the property name
            var propertyName = leftMemEx.Member.Name;

            // Get a setter function for the property
            var propInfo = leftTarget.GetType().GetProperties().First(p => p.Name == propertyName);
            Action<object, object> setter = (target, value) => propInfo.GetSetMethod().Invoke(target, new[] { value });

            // Create an action that is called whenever one of the propertys in the right expression is changing
            Action rightChangedAction = () => setter(leftTarget, EvalExpression(right));

            // Get all right side triggers and create a complex binding (IDisposable) from it
            return SubscribeToRightSidePropertys(rightChangedAction, RightTriggerFromComplexExpression(right));
        }

        /// <summary>
        /// The subscribe to right side properties.
        /// </summary>
        /// <param name="rightChangedAction">
        /// The right changed action.
        /// </param>
        /// <param name="rightTrigger">
        /// The right trigger.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        static IDisposable SubscribeToRightSidePropertys(Action rightChangedAction, List<ExpressionData> rightTrigger)
        {
            var typeNotifyPropertyChanged = typeof(INotifyPropertyChanged);
            var disposable = new DisposableContainer();

            // For all properties on the right side
            foreach (var expr in rightTrigger)
            {
                // IF this is a property access expression
                if (expr.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    var memEx = expr.Expression as MemberExpression;

                    // Get the type this property belongs too and check if it is implementing INotifyPropertyChanged
                    if (typeNotifyPropertyChanged.GetType().IsAssignableFrom(memEx.Type.GetType()))
                    {
                        // Register at PropertyChangedEventHandler and add the unsubscribing IDisposable to the container.
                        disposable.AddDisposable(
                            AddChangeNotificationEventHandler(
                                EvalExpression(memEx), // get instance that declares the current right side property
                                expr.Info.Name, // the property name where to subscribe to property changed
                                rightChangedAction));

                        // the action that is updating the left side property when right side changes
                    }
                }
            }

            return disposable;
        }

        #endregion

        #endregion

        #region AddChangeNotificationEventHandler action to target with given property name

        /// <summary>
        /// The add change notification event handler.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        static IDisposable AddChangeNotificationEventHandler(object target, string propertyName, Action action)
        {
            IDisposable binding = null;
            var npc = target as INotifyPropertyChanged;
            if (npc != null)
            {
                PropertyChangedEventHandler handler = (obj, args) =>
                {
                    if (args.PropertyName == propertyName)
                    {
                        action();
                    }
                };
                npc.PropertyChanged += handler;
                binding = Disposable.Create(() => npc.PropertyChanged -= handler);
            }

            return binding;
        }

        #endregion

        #region Get left and right triggers from simple and complex expressions

        /// <summary>
        /// The left trigger from member expression.
        /// </summary>
        /// <param name="expr">
        /// The expr.
        /// </param>
        /// <returns>
        /// The <see cref="Tuple"/>.
        /// </returns>
        static ExpressionData LeftTriggerFromMemberExpression(Expression expr)
        {
            // This expression represents a field or property of an instance.
            var m = (MemberExpression)expr;
            return new ExpressionData(m.Expression, m.Member);
        }

        /// <summary>
        /// The right trigger from complex expression.
        /// </summary>
        /// <param name="expr">
        /// The expr.
        /// </param>
        /// <param name="ts">
        /// The ts.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        static List<ExpressionData> RightTriggerFromComplexExpression(Expression expr, List<ExpressionData> ts = null)
        {
            var triggers = ts == null ? new List<ExpressionData>() : ts;
            GetBindingMembers(expr, triggers);
            return triggers;
        }

        /// <summary>
        /// The get binding members.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <param name="triggers">
        /// The triggers.
        /// </param>
        static void GetBindingMembers(Expression s, List<ExpressionData> triggers)
        {
            if (s.NodeType == ExpressionType.MemberAccess)
                triggers.Add(LeftTriggerFromMemberExpression(s));
            else if (s.NodeType == ExpressionType.Call)
            {
                var b = s as MethodCallExpression;
                if (b != null)
                {
                    b.Arguments.ForEach(argExpr => GetBindingMembers(argExpr, triggers));
                }
            }
            else
            {
                var b = s as BinaryExpression;
                if (b != null)
                {
                    GetBindingMembers(b.Left, triggers);
                    GetBindingMembers(b.Right, triggers);
                }
            }
        }

        #endregion

        #endregion

        #region Private class BindingMember for simple binding case

        /// <summary>
        /// The binding member.
        /// </summary>
        class BindingMember
        {
            /// <summary>
            /// The from expression.
            /// </summary>
            /// <param name="memEx">
            /// The mem ex.
            /// </param>
            /// <returns>
            /// The <see cref="BindingMember"/>.
            /// </returns>
            public static BindingMember FromExpression(MemberExpression memEx)
            {
                var target = EvalExpression(memEx.Expression);
                var propName = memEx.Member.Name;
                var propInfo = target.GetType().GetProperties().First(p => p.Name == propName);

                return new BindingMember()
                {
                    Target = target,
                    PropertyName = propName,
                    GetMethod = () => propInfo.GetGetMethod().Invoke(target, null),
                    SetMethod = obj => propInfo.GetSetMethod().Invoke(target, new[] { obj })
                };
            }

            /// <summary>
            /// Gets or sets the target.
            /// </summary>
            public object Target { get; set; }

            /// <summary>
            /// Gets or sets the property name.
            /// </summary>
            public string PropertyName { get; set; }

            /// <summary>
            /// Gets or sets the get method.
            /// </summary>
            public Func<object> GetMethod { get; set; }

            /// <summary>
            /// Gets or sets the set method.
            /// </summary>
            public Action<object> SetMethod { get; set; }
        }

        #endregion

        /// <summary>
        /// The eval expression.
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object EvalExpression(Expression operation)
        {
            object value;
            if (!TryEvaluate(operation, out value))
            {
                // use compile / invoke as a fall-back
                value = Expression.Lambda(operation).Compile().DynamicInvoke();
            }

            return value;
        }

        /// <summary>
        /// The try evaluate.
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool TryEvaluate(Expression operation, out object value)
        {
            if (operation == null)
            {   // used for static fields, etc
                value = null;
                return true;
            }

            switch (operation.NodeType)
            {
                case ExpressionType.Constant:
                    value = ((ConstantExpression)operation).Value;
                    return true;
                case ExpressionType.MemberAccess:
                    var me = (MemberExpression)operation;
                    object target;
                    if (TryEvaluate(me.Expression, out target))
                    { // instance target
                        if (me.Member is FieldInfo)
                        {
                            value = ((FieldInfo)me.Member).GetValue(target);
                            return true;
                        }
                        else if (me.Member is PropertyInfo)
                        {
                            value = ((PropertyInfo)me.Member).GetValue(target, null);
                            return true;
                        }
                    }

                    break;
            }

            value = null;
            return false;
        }
    }

    #region Helper 

    /// <summary>
    /// The enumerable extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// The for each.
        /// </summary>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }

    /// <summary>
    /// The DisposableContainer interface.
    /// </summary>
    public interface IDisposableContainer : IDisposable
    {
        /// <summary>
        /// Gets the disposables.
        /// </summary>
        List<IDisposable> Disposables { get; }
    }

    /// <summary>
    /// The i disposable container extensions.
    /// </summary>
    public static class IDisposableContainerExtensions
    {
        /// <summary>
        /// The add disposable.
        /// </summary>
        /// <param name="subscriptable">
        /// The subscriptable.
        /// </param>
        /// <param name="disposable">
        /// The disposable.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void AddDisposable<T>(this T subscriptable, IDisposable disposable)
            where T : IDisposableContainer
        {
            if (subscriptable != null && subscriptable.Disposables != null)
            {
                subscriptable.Disposables.Add(disposable);
            }
        }

        /// <summary>
        /// The add disposable.
        /// </summary>
        /// <param name="disposableContainer">
        /// The disposable container.
        /// </param>
        /// <param name="onDispose">
        /// The on dispose.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void AddDisposable<T>(this T disposableContainer, Action onDispose)
            where T : IDisposableContainer
        {
            if (onDispose != null)
            {
                disposableContainer.Disposables.Add(Disposable.Create(onDispose));
            }
        }

        /// <summary>
        /// The dispose subscriptions.
        /// </summary>
        /// <param name="subscriptable">
        /// The subscriptable.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void DisposeSubscriptions<T>(this T subscriptable)
            where T : IDisposableContainer
        {
            if (subscriptable.Disposables != null)
            {
                subscriptable.Disposables.ForEach(s => s.Dispose());
            }
        }

    }

    /// <summary>
    /// The disposable container.
    /// </summary>
    public class DisposableContainer : IDisposableContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableContainer"/> class.
        /// </summary>
        /// <param name="onDispose">
        /// The on dispose.
        /// </param>
        /// <param name="disposables">
        /// The disposables.
        /// </param>
        public DisposableContainer(Action onDispose = null, params IDisposable[] disposables)
        {
            if (onDispose != null)
            {
                this.AddDisposable(onDispose);
            }

            disposables.ForEach(d => this.AddDisposable(d));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableContainer"/> class.
        /// </summary>
        /// <param name="onDispose">
        /// The on dispose.
        /// </param>
        public DisposableContainer(params Action[] onDispose)
        {
            onDispose.ForEach(od => this.AddDisposable(od));
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.DisposeSubscriptions();
        }

        /// <summary>
        /// Gets the disposables.
        /// </summary>
        public List<IDisposable> Disposables
        {
            get;
            private set;
        }
    }

    /// <summary>
    /// The delegate disposable.
    /// </summary>
    public class DelegateDisposable : IDisposable
    {
        /// <summary>
        /// Gets or sets the on dispose.
        /// </summary>
        public Action OnDispose { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateDisposable"/> class.
        /// </summary>
        /// <param name="onDispose">
        /// The on dispose.
        /// </param>
        public DelegateDisposable(Action onDispose = null)
        {
            if (onDispose != null)
            {
                OnDispose = onDispose;
            }
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            if (OnDispose != null)
            {
                OnDispose();
            }
        }
    }

    /// <summary>
    /// The disposable.
    /// </summary>
    public static class Disposable
    {
        /// <summary>
        /// The as disposable.
        /// </summary>
        /// <param name="onDispose">
        /// The on dispose.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public static IDisposable AsDisposable(this Action onDispose)
        {
            return new DelegateDisposable(onDispose);
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="onDispose">
        /// The on dispose.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public static IDisposable Create(Action onDispose)
        {
            return new DelegateDisposable(onDispose);
        }

#if !UNITY_5
        public static IDisposable Create<T>(T reference, Action<T> onDispose)
           where T : class
        {
            var weakReference = new WeakReference(reference);
            return new DelegateDisposable(() =>
            {
                var target = (T)weakReference.Target;
                if (target != null)
                {
                    onDispose(target);
                }
            });
        }
#endif

        /// <summary>
        /// The create container.
        /// </summary>
        /// <param name="onDispose">
        /// The on dispose.
        /// </param>
        /// <returns>
        /// The <see cref="DisposableContainer"/>.
        /// </returns>
        public static DisposableContainer CreateContainer(params Action[] onDispose)
        {
            return new DisposableContainer(onDispose);
        }

        /// <summary>
        /// The create container.
        /// </summary>
        /// <param name="disposables">
        /// The disposables.
        /// </param>
        /// <returns>
        /// The <see cref="DisposableContainer"/>.
        /// </returns>
        public static DisposableContainer CreateContainer(params IDisposable[] disposables)
        {
            return new DisposableContainer(null, disposables);
        }
    }

    #endregion
}

