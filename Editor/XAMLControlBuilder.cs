/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace CBX.Utilities
{
    using System.Reflection;
    using System.Security;
    
    public class XAMLControlBuilder
    {
        /*
          public string Instructions { get; set; }
          public ContentManager ContentManager { get; set; }

          public Control Build(Manager manager)
          {
              return this.Build(manager, true, false);
          }

          public Control Build(Manager manager, bool autoAddWindow, bool autoAddControl)
          {
              var doc = XDocument.Parse(this.Instructions);
              switch (doc.Root.Name.LocalName)
              {
                  case "Window":
                      // try to find a type attribute
                      var win = this.CreateType<Window>(doc.Root, manager);
                      win.Init();
                      var att = doc.Root.Attribute("Title");
                      win.Text = att != null ? att.Value : string.Empty;
                      att = doc.Root.Attribute("Width");
                      win.Width = att != null ? int.Parse(att.Value) : win.Width;
                      att = doc.Root.Attribute("Height");
                      win.Height = att != null ? int.Parse(att.Value) : win.Height;

                      ProcessCommonProperties(doc.Root, win);
                      ProcessControlEvents(doc.Root, win, win);

                      this.BuildChildren(manager, win, win, doc.Root.Elements());
                      if (autoAddWindow) manager.Add(win);
                      return win;

                  case "UserControl":
                      // try to find a type attribute
                      var ctrl = this.CreateType<Control>(doc.Root, manager);
                      ctrl.Init();
                      att = doc.Root.Attributes().Where(x => x.Name.LocalName == "DesignWidth").FirstOrDefault();
                      ctrl.Width = att != null ? int.Parse(att.Value) : ctrl.Width;
                      att = doc.Root.Attributes().Where(x => x.Name.LocalName == "DesignHeight").FirstOrDefault();
                      ctrl.Height = att != null ? int.Parse(att.Value) : ctrl.Height;

                      ProcessCommonProperties(doc.Root, ctrl);
                      ProcessControlEvents(doc.Root, ctrl, ctrl);

                      this.BuildChildren(manager, ctrl, ctrl, doc.Root.Elements());
                      if (autoAddControl) manager.Add(ctrl);
                      return ctrl;
              }

              return null;
          }

          private T CreateType<T>(XElement node, Manager manager)
          {
              T win;
              var typeAtt = node.Attributes().Select(x => x.Name.LocalName == "Class" ? x : null).FirstOrDefault();
              if (typeAtt != null)
              {
                  var types = from asm in AppDomain.CurrentDomain.GetAssemblies()
                              from ty in asm.GetTypes()
                              where ty.FullName == typeAtt.Value
                              select ty;

                  var t = types.FirstOrDefault();
                  if (t != null)
                  {
                      win = (T)Activator.CreateInstance(t, manager);
                  }
                  else
                  {
                      throw new TypeInitializationException(typeAtt.Value, new Exception("Could not create type."));
                      // win = new Window(manager);
                  }
              }
              else
              {
                  throw new TypeInitializationException(typeAtt.Value, new Exception("Could not create type."));
                  // win = new Window(manager);
              }
              return win;
          }

          internal void BuildChildren(Manager manager, Control parent, Control rootControl, IEnumerable<XElement> elements)
          {
              foreach (var node in elements)
              {
                  Control ctrl = null;
                  switch (node.Name.LocalName)
                  {
                      case "Grid":
                          ctrl = new Container(manager);
                          ctrl.Anchor = Anchors.All;
                          ctrl.Left = 0;
                          ctrl.Top = 0;
                          ctrl.Width = parent.ClientWidth;
                          ctrl.Height = parent.ClientHeight;
                          break;

                      case "Button":
                          if (parent is ToolBar)
                          {
                              ctrl = new ToolBarButton(manager);
                          }
                          else
                          {
                              ctrl = new Button(manager);
                          }
                          break;

                      case "Border":
                          ctrl = new Bevel(manager);
                          break;

                      case "StackPanel":
                          ctrl = new StackPanel(manager, Orientation.Vertical);
                          var att = node.Attribute("Orientation");
                          if (att != null)
                          {
                              Orientation ori;
                              if (!Enum.TryParse(att.Value, out ori))
                              {
                                  // ERR: Need to handle neoforce image size mode from xaml conversion here
                                  // throw new XmlException("Unsupported XAML Image control property value 'Stretch'. Expected 'None' or 'Fill'.");
                              }
                              var sp =(StackPanel) ctrl;
                              sp.Orientation = ori;

                          }
                          break;
                    
                      case "GroupBox":
                          ctrl = new GroupBox(manager);
                          var hAtt = node.Attribute("Header");
                          ctrl.Text = hAtt != null ? hAtt.Value : ctrl.Text;
                          break;

                      case "TextBlock":
                      case "Label":
                          ctrl = new Label(manager);
                          break;

                      case "TextBox":
                          ctrl = new TextBox(manager);
                          ProcessEvent(node, ctrl, rootControl, "TextChanged");
                          var mLinesAtt = node.Attribute("MaxLines");
                          var txt = (TextBox)ctrl;
                          var textBoxMode = TextBoxMode.Normal;
                          if (mLinesAtt != null) textBoxMode = int.Parse(mLinesAtt.Value) > 1 ? TextBoxMode.Multiline : TextBoxMode.Normal;
                          txt.Mode = textBoxMode;
                          break;

                      case "PasswordBox":
                          ctrl = new TextBox(manager);
                          ProcessEvent(node, ctrl, rootControl, "PasswordChanged", "TextChanged");
                          txt = (TextBox)ctrl;
                          txt.Mode = TextBoxMode.Password;
                          var passAtt = node.Attribute("PasswordChar");
                          txt.PasswordChar = passAtt != null ? passAtt.Value[0] : txt.PasswordChar;
                          break;

                      case "CheckBox":
                          ctrl = new CheckBox(manager);
                          att = node.Attribute("IsChecked");
                          ((CheckBox)ctrl).Checked = att != null ? bool.Parse(att.Value) : true;
                          break;

                      case "RadioButton":
                          ctrl = new RadioButton(manager);
                          att = node.Attribute("IsChecked");
                          ((RadioButton)ctrl).Checked = att != null ? bool.Parse(att.Value) : true;
                          break;

                      case "ComboBox":
                          ctrl = new ComboBox(manager);
                          var cboBox = (ComboBox)ctrl;
                          // add items
                          var cbItems = (from item in node.Elements()
                                         where item.Name.LocalName == "ComboBoxItem"
                                         let attrib = item.Attribute("Content")
                                         let sel = item.Attribute("IsSelected")
                                         where attrib != null
                                         select new { Value = attrib.Value, Selected = sel != null && sel.Value == "True" ? true : false }).ToArray();

                          for (var i = 0; i < cbItems.Length; i++)
                          {
                              var item = cbItems[i];
                              cboBox.Items.Add(item.Value);
                              if (cbItems[i].Selected) cboBox.ItemIndex = i;
                          }

                          ProcessEvent(node, ctrl, rootControl, "SelectionChanged", "ItemIndexChanged");
                          break;

                      case "ListBox":
                          ctrl = new ListBox(manager);
                          var lstBox = (ListBox)ctrl;
                          // add items
                          var lbItems = (from item in node.Elements()
                                         where item.Name.LocalName == "ListBoxItem"
                                         let attrib = item.Attribute("Content")
                                         let sel = item.Attribute("IsSelected")
                                         where attrib != null
                                         select new { Value = attrib.Value, Selected = sel != null && sel.Value == "True" ? true : false }).ToArray();

                          for (var i = 0; i < lbItems.Length; i++)
                          {
                              var item = lbItems[i];
                              lstBox.Items.Add(item.Value);
                              if (lbItems[i].Selected) lstBox.ItemIndex = i;
                          }

                          ProcessEvent(node, ctrl, rootControl, "SelectionChanged", "ItemIndexChanged");
                          break;

                      case "Image":
                          ctrl = new ImageBox(manager);
                          var ib = (ImageBox)ctrl;
                          // attempt to content.load from uid first
                          att = node.Attribute("Uid");
                          if (att != null)
                          {
                              ib.Image = this.ContentManager != null ? this.ContentManager.Load<Texture2D>(att.Value) : null;
                          }
                          else
                          {
                              // try to load from valid file uri
                              att = node.Attribute("Source");
                              if (att != null)
                              {
                                  Uri url;
                                  if (Uri.TryCreate(att.Value, UriKind.RelativeOrAbsolute, out url))
                                  {
                                      try
                                      {
                                          if (url.IsFile)
                                          {
                                              var image = Texture2D.FromStream(manager.GraphicsDevice, System.IO.File.OpenRead(url.PathAndQuery));
                                              ib.Image = image;
                                          }
                                          else
                                          {
                                              // ERR: Need to handle neoforce image source from xaml conversion here currently only supports file uri
                                          }
                                      }
                                      catch
                                      {
                                          //ERR: Need to handle neoforce image source parse error from xmal data
                                      }
                                  }
                              }
                          }

                          att = node.Attribute("Stretch");
                          if (att != null)
                          {
                              switch (att.Value)
                              {
                                  case "None":
                                      ib.SizeMode = SizeMode.Normal;
                                      break;
                                  case "Fill":
                                      ib.SizeMode = SizeMode.Stretched;
                                      break;
                                  default:
                                      // ERR: Need to handle neoforce image size mode from xaml conversion here
                                      // throw new XmlException("Unsupported XAML Image control property value 'Stretch'. Expected 'None' or 'Fill'.");
                                      break;
                              }
                          }

                          ProcessEvent(node, ctrl, rootControl, "SourceUpdated", "ImageChanged");
                          break;

                      case "ProgressBar":
                          ctrl = new ProgressBar(manager);
                          var pb = (ProgressBar)ctrl;
                          var maxAtt = node.Attribute("Maximum");
                          var minAtt = node.Attribute("Minimum");
                          var vAtt = node.Attribute("Value");
                          if (maxAtt != null && minAtt != null)
                          {
                              pb.Range = int.Parse(maxAtt.Value) - int.Parse(minAtt.Value);
                          }
                          pb.Value = vAtt != null ? int.Parse(vAtt.Value) : pb.Value;

                          ProcessEvent(node, ctrl, rootControl, "ValueChanged");
                          break;

                      case "Slider":
                          ctrl = new TrackBar(manager);
                          var tb = (TrackBar)ctrl;
                          maxAtt = node.Attribute("Maximum");
                          minAtt = node.Attribute("Minimum");
                          if (maxAtt != null && minAtt != null)
                          {
                              tb.Range = int.Parse(maxAtt.Value) - int.Parse(minAtt.Value);
                          }
                          att = node.Attribute("LargeChange");
                          tb.PageSize = att != null ? int.Parse(att.Value) : tb.PageSize;
                          att = node.Attribute("SmallChange");
                          tb.StepSize = att != null ? int.Parse(att.Value) : tb.StepSize;
                          att = node.Attribute("Value");
                          tb.Value = att != null ? int.Parse(att.Value) : tb.Value;

                          ProcessEvent(node, ctrl, rootControl, "ValueChanged");

                          break;

                      case "ScrollBar":
                          att = node.Attribute("Orientation");
                          if (att == null) throw new XmlSyntaxException("ScrollBar control did not contain a orientation attribute.");
                          Orientation orient;
                          if (!Orientation.TryParse(att.Value, out orient)) throw new XmlSyntaxException("Could not determine ScrollBar orientation attribute value.");

                          ctrl = new ScrollBar(manager, orient);
                          var sb = (ScrollBar)ctrl;
                          maxAtt = node.Attribute("Maximum");
                          minAtt = node.Attribute("Minimum");
                          if (maxAtt != null && minAtt != null)
                          {
                              sb.Range = int.Parse(maxAtt.Value) - int.Parse(minAtt.Value);
                          }
                          att = node.Attribute("LargeChange");
                          sb.PageSize = att != null ? int.Parse(att.Value) : sb.PageSize;
                          att = node.Attribute("SmallChange");
                          sb.StepSize = att != null ? int.Parse(att.Value) : sb.StepSize;
                          att = node.Attribute("Value");
                          sb.Value = att != null ? int.Parse(att.Value) : sb.Value;

                          ProcessEvent(node, ctrl, rootControl, "ValueChanged");

                          break;

                      case "TabControl":
                          ctrl = new TabControl(manager);
                          break;

                      case "TabItem":
                          if (!(parent is TabControl)) break;
                          var tc = (TabControl)parent;
                          att = node.Attribute("Header");
                          ctrl = tc.AddPage(att == null ? string.Empty : att.Value);
                          break;

                      case "Menu":
                          ctrl = new MainMenu(manager);
                          ProcessMenuItems((MainMenu)ctrl, null, rootControl, node.Elements());
                          break;

                      case "StatusBar":
                          ctrl = new StatusBar(manager);
                          break;

                      case "ToolBarPanel":
                          ctrl = new ToolBarPanel(manager);
                          break;

                      case "ToolBar":
                          ctrl = new ToolBar(manager);
                          break;

                      default:
                          att = node.Attribute("Uid");
                          if (att == null) break;
                          var builder = manager.Game.Content.Load<XAMLControlBuilder>(att.Value);
                          builder.ContentManager = this.ContentManager;
                          ctrl = builder.Build(manager);
                          break;
                  }

                  if (ctrl != null)
                  {
                      ctrl.Init();
                      ctrl.Parent = parent;

                      ProcessCommonProperties(node, ctrl);
                      ParsePositionAndSize(node, parent, ctrl);
                      ProcessControlEvents(node, rootControl, ctrl);

                      this.BuildChildren(manager, ctrl, rootControl, node.Elements());

                      if (node.Name.LocalName == "TabControl")
                      {
                          var tc = (TabControl)ctrl;
                          var att = node.Attribute("SelectedIndex");
                          tc.SelectedIndex = att != null ? int.Parse(att.Value) : tc.SelectedIndex;
                      }
                  }
              }
          }

          private void ProcessMenuItems(MainMenu mainMenu, MenuItem parent, Control rootControl, IEnumerable<XElement> elements)
          {
              bool doSeparate = false;
              foreach (var node in elements)
              {
                  switch (node.Name.LocalName)
                  {
                      case "MenuItem":
                          var item = new MenuItem();
                          if (doSeparate) item.Separated = true;
                          doSeparate = false;
                          var att = node.Attribute("Header");
                          if (att != null) item.Text = att.Value;
                          // enabled
                          att = node.Attribute("IsEnabled");
                          item.Enabled = att != null ? bool.Parse(att.Value) : true;

                          int index = item.Text.IndexOf("_");
                          if (index != -1) item.Text = item.Text.Remove(index, 1);

                          att = node.Attribute("Uid");
                          if (att != null) item.Image = this.ContentManager != null ? this.ContentManager.Load<Texture2D>(att.Value) : null;

                          att = node.Attribute("Tag");
                          if (att != null) item.Tag = att.Value;

                          if (parent == null)
                          {
                              mainMenu.Items.Add(item);
                          }
                          else
                          {
                              parent.Items.Add(item);
                          }

                          ProcessMenuItemEvent(node, item, rootControl, "Click");

                          this.ProcessMenuItems(mainMenu, item, rootControl, node.Elements());
                          break;

                      case "Separator":
                          doSeparate = true;
                          break;
                  }
              }
          }

          private static void ProcessCommonProperties(XElement node, Control ctrl)
          {
              // Text
              var att = node.Attribute("Content");
              if (att == null) att = node.Attribute("Text");
              if (att != null) ctrl.Text = att.Value;
              // Tooltip
              att = node.Attribute("ToolTip");
              if (att != null && ctrl.ToolTip != null) ctrl.ToolTip.Text = att.Value;
              // Tag/Uid
              att = node.Attribute("Uid");
              if (att != null && ctrl.Tag != null) ctrl.Tag = att.Value;
              // ctrl name
              //att = node.Attribute("Name");
              att = node.Attributes().Where(x => x.Name.LocalName == "Name").FirstOrDefault();
              if (att != null) ctrl.Name = att.Value;
              // ctrl tag
              att = node.Attribute("Tag");
              if (att != null) ctrl.Tag = att.Value;
              // visible
              att = node.Attribute("Visibility");
              if (att != null)
              {
                  if (att.Value != "Visible") ctrl.Hide(); else ctrl.Show();
              }
              // visible
              att = node.Attribute("IsEnabled");
              ctrl.Enabled = att != null ? bool.Parse(att.Value) : true;
              // min/max sizes
              att = node.Attribute("MinWidth");
              ctrl.MinimumWidth = att != null ? int.Parse(att.Value) : ctrl.MinimumWidth;
              att = node.Attribute("MinHeight");
              ctrl.MinimumHeight = att != null ? int.Parse(att.Value) : ctrl.MinimumHeight;
              att = node.Attribute("MaxWidth");
              ctrl.MaximumWidth = att != null ? int.Parse(att.Value) : ctrl.MaximumWidth;
              att = node.Attribute("MaxHeight");
              ctrl.MaximumHeight = att != null ? int.Parse(att.Value) : ctrl.MaximumHeight;
          }

          private static void ProcessControlEvents(XElement node, Control rootControl, Control ctrl)
          {
              ProcessEvent(node, ctrl, rootControl, "Click");
              ProcessEvent(node, ctrl, rootControl, "GotFocus", "FocusGained");
              ProcessEvent(node, ctrl, rootControl, "LostFocus", "FocusLost");
              ProcessEvent(node, ctrl, rootControl, "KeyDown");
              ProcessEvent(node, ctrl, rootControl, "KeyUp");
              ProcessEvent(node, ctrl, rootControl, "MouseDown");
              ProcessEvent(node, ctrl, rootControl, "MouseMove");
              ProcessEvent(node, ctrl, rootControl, "MouseUp");
              ProcessEvent(node, ctrl, rootControl, "MouseEnter", "MouseOver");
              ProcessEvent(node, ctrl, rootControl, "MouseLeave", "MouseOut");
              ProcessEvent(node, ctrl, rootControl, "SizeChanged", "Resize");
              ProcessEvent(node, ctrl, rootControl, "IsEnabledChanged", "EnabledChanged");
              ProcessEvent(node, ctrl, rootControl, "IsVisibleChanged", "VisibleChanged");
          }

          private static void ParsePositionAndSize(XElement node, Control parent, Control ctrl)
          {
              // process anchor first
              var anchor = ctrl.Anchor;
              ProcessAnchorNode(node, ref anchor);

              // process location and size next
              var wAtt = node.Attribute("Width");
              var hAtt = node.Attribute("Height");
              //  ctrl.Width = wAtt != null ? int.Parse(wAtt.Value) : ctrl.Width; 
              //  ctrl.Height = hAtt != null ? int.Parse(hAtt.Value) : ctrl.Height;

              var mAtt = node.Attribute("Margin");
              if (mAtt != null)
              {
                  var parts = mAtt.Value.Split(new[] { ',' }).Select(p => int.Parse(p)).ToArray();

                  ctrl.Left = parent.ClientLeft + parts[0];
                  ctrl.Top = parent.ClientTop + parts[1];
                  ctrl.Width = wAtt != null ? int.Parse(wAtt.Value) : parent.ClientWidth - (parts[0] - parts[2]);
                  ctrl.Height = hAtt != null ? int.Parse(hAtt.Value) : parent.ClientHeight - (parts[1] - parts[3]);

                  if (anchor.HasFlag(Anchors.Left)) ctrl.Left = parent.ClientLeft + parts[0];
                  if (anchor.HasFlag(Anchors.Top)) ctrl.Top = parent.ClientTop + parts[1];

                  //  if (anchor.HasFlag(Anchors.Right)) ctrl.Width = parent.ClientWidth - (parts[0] - parts[2]) - ctrl.Width;
                  //  if (anchor.HasFlag(Anchors.Bottom)) ctrl.Top = parent.ClientHeight - (parts[1] - parts[3]) - ctrl.Height;
                  if (anchor.HasFlag(Anchors.Right) && wAtt == null) ctrl.Width = parent.ClientWidth - (parts[0] + parts[2]);
                  if (anchor.HasFlag(Anchors.Bottom) && hAtt == null) ctrl.Height = parent.ClientHeight - (parts[1] + parts[3]);
                  if (anchor.HasFlag(Anchors.Right) && wAtt != null) ctrl.Left = parent.ClientWidth - parts[2] - ctrl.Width;
                  if (anchor.HasFlag(Anchors.Bottom) && hAtt != null) ctrl.Top = parent.ClientHeight - parts[3] - ctrl.Height;
                  //            
                  //  if (anchor.HasFlag(Anchors.Right) && wAtt == null) ctrl.Left = parent.ClientWidth - (parts[2] + ctrl.Width);
                  //if (anchor.HasFlag(Anchors.Bottom) && hAtt == null) ctrl.Top = parent.ClientHeight - (parts[3] + ctrl.Height);
              }

              ctrl.Anchor = anchor;
          }

          private static void ProcessEvent(XElement node, Control ctrl, Control rootControl, string eventName)
          {
              ProcessEvent(node, ctrl, rootControl, eventName, eventName);
          }

          private static void ProcessEvent(XElement node, Control ctrl, Control rootControl, string eventName, string ctrlEventName)
          {
              // try to get the click node
              var clickNode = node.Attribute(eventName);
              if (clickNode != null && rootControl != null)
              {
                  var clickMe = from cm in rootControl.GetType().GetMethods(BindingFlags.Instance |
                                                                              BindingFlags.NonPublic |
                                                                              BindingFlags.Public |
                                                                              BindingFlags.DeclaredOnly)
                                where cm.MemberType == MemberTypes.Method &&
                                      cm.DeclaringType == rootControl.GetType() &&
                                      string.Compare(cm.Name, clickNode.Value, true) == 0
                                select cm;

                  foreach (System.Reflection.MethodInfo info in clickMe)
                  {
                      // get event info
                      var evClick = ctrl.GetType().GetEvent(ctrlEventName);
                      var tDelegate = evClick.EventHandlerType;
                      var d = Delegate.CreateDelegate(tDelegate, rootControl, info);

                      // hook up event
                      var addHandler = evClick.GetAddMethod();
                      Object[] addHandlerArgs = { d };
                      addHandler.Invoke(ctrl, addHandlerArgs);

                      break;
                  }
              }
          }

          private static void ProcessMenuItemEvent(XElement node, MenuItem ctrl, Control rootControl, string eventName)
          {
              ProcessMenuItemEvent(node, ctrl, rootControl, eventName, eventName);
          }

          private static void ProcessMenuItemEvent(XElement node, MenuItem ctrl, Control rootControl, string eventName, string ctrlEventName)
          {
              // try to get the click node
              var clickNode = node.Attribute(eventName);
              if (clickNode != null && rootControl != null)
              {
                  var clickMe = from cm in rootControl.GetType().GetMethods(BindingFlags.Instance |
                                                                              BindingFlags.NonPublic |
                                                                              BindingFlags.Public |
                                                                              BindingFlags.DeclaredOnly)
                                where cm.MemberType == MemberTypes.Method &&
                                      cm.DeclaringType == rootControl.GetType() &&
                                      string.Compare(cm.Name, clickNode.Value, true) == 0
                                select cm;

                  foreach (System.Reflection.MethodInfo info in clickMe)
                  {
                      // get event info
                      var evClick = ctrl.GetType().GetEvent(ctrlEventName);
                      var tDelegate = evClick.EventHandlerType;
                      var d = Delegate.CreateDelegate(tDelegate, rootControl, info);

                      // hook up event
                      var addHandler = evClick.GetAddMethod();
                      Object[] addHandlerArgs = { d };
                      addHandler.Invoke(ctrl, addHandlerArgs);

                      break;
                  }
              }
          }

          private static void ProcessAnchorNode(XElement inputNode, ref Anchors anchor)
          {
              var haNode = inputNode.Attribute("HorizontalAlignment");
              var vaNode = inputNode.Attribute("VerticalAlignment");

              var xAtt = Anchors.None;
              var yAtt = Anchors.None;
              var rAtt = Anchors.None;
              var bAtt = Anchors.None;
              if (haNode != null)
              {
                  switch (haNode.Value)
                  {
                      case "Left":
                          xAtt = Anchors.Left;
                          rAtt = Anchors.None;
                          break;
                      case "Center":
                          xAtt = Anchors.None;
                          rAtt = Anchors.None;
                          break;
                      case "Right":
                          xAtt = Anchors.None;
                          rAtt = Anchors.Right;
                          break;
                      case "Stretch":
                          xAtt = Anchors.Left;
                          rAtt = Anchors.Right;
                          break;
                      default:
                          xAtt = Anchors.Left;
                          rAtt = Anchors.Right;
                          break;
                  }
              }

              if (vaNode != null)
              {
                  switch (vaNode.Value)
                  {
                      case "Top":
                          yAtt = Anchors.Top;
                          bAtt = Anchors.None;
                          break;
                      case "Center":
                          yAtt = Anchors.None;
                          bAtt = Anchors.None;
                          break;
                      case "Bottom":
                          yAtt = Anchors.None;
                          bAtt = Anchors.Bottom;
                          break;
                      case "Stretch":
                          yAtt = Anchors.Top;
                          bAtt = Anchors.Bottom;
                          break;
                      default:
                          yAtt = Anchors.Top;
                          bAtt = Anchors.Bottom;
                          break;
                  }
              }

              var tmp = xAtt | yAtt | rAtt | bAtt;
              if (haNode == null && vaNode == null)
              {
                  // anchor = val;   // return same value that was passed in
              }
              else
              {
                  anchor = tmp;
              }
          }

          */
    }
}
