   M S B A M L   `   `   `  ���� D  @PicEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PicEditor.StylesPicEditor.Styles  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Z VDevExpress.Mvvm.UI, Version=19.2.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ContextMenuStyle  �� 
TargetType �� Property  MainColorBrush �� Value  ForegroundBrush �� 
TargetType �� Padding J� SnapsToDevicePixels  Menu.Static.Background  	MainColor �� Color  	MenuStyle  MenuItemControlTemplate ��Name �Name	 � ColumnDefinitions
 ��Name m� Icon F�Name �� HeaderStringFormat �� ContentStringFormat �Name � AllowsTransparency �� RelativeSource ��Name �� 
ResourceId �� TypeInTargetAssembly �� ClearTypeHint ��Name ��Name �� ElementName ��Name-   'System.Windows.Input.KeyboardNavigation    DirectionalNavigation � IsSharedSizeScope    TabNavigation �� Triggers P� Property P� Value  �� 
TargetName! P� 
SourceName   ArrowMenuItemControlTemplate" z� SharedSizeGroup# ��Name$ J� Opacity% m� InputGestureText& � HorizontalOffset' � VerticalOffset�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     (localclr-namespace:PicEditor.Styles  8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  %�  &	        &	 �     &	      &	 �    &	 _	    �� "  ��.���  � ��# �  ��  � ��# � ����  � ��# � ����  � ��# � ����  � ��# � ����  � ��# �  ��  � ��$ Center=���  � �� �� " ��.R��� #���  #��z��#���  # z�� . ._��� # z �� # �  �� "  o�.���  � ��# �  ��  � ��# � ����  � ��# � ����  � ��# � ����  � ��# � ����  � ��# �  ��  � ��$ Center=���  � �� �� " o�.R��� ���	#FF999999#��z��#���  # z�� . ._��� # z �� " m�.R��� $ templateRoot��#��z��#��z��#��z�� . ._�� $
 Grid��$��Center=�	 �� $
��Auto ��� $
��Auto �.+��� $

 Icon��#��z $
��Icon��$��Center=�$��16��$��3q�# z $��Center=�$��16��F� $ 
GlyphPanel��T������� �O �`�G �ڊ � �D �ڊ �    �XO � � �g5 ���A  ] ������� @    р�� �O #��z��$��LeftToRight=�$��3q�$y�	Collapsed=�$��Center=��� $
 ContentPresenter��#��z��#��z��$��1��# z $��Header��#��z����. # z � $ 
PART_Popup�� . |�.  #��� -�$��Bottom=����� *IsSubmenuOpen+ �� *TemplatedParent+.���� $ SubMenuBorder�����	#FF999999$��1q�#���  $ 2q�._��� $ SubMenuScrollViewer����C� *�� $ MenuScrollViewer��" �+.��� $ Grid1��$ Enabled=�.+��� $ Canvas��$
��Left=�$��0��$	��Top=�$��0��.+��� $ 
OpaqueRect������ *
Background+$ SubMenuBorder������ *ActualHeight+$ SubMenuBorder������ *ActualWidth+$ SubMenuBorder���� $ 	Rectangle��#���  $
��Left=�$��29,2,0,2q�$��1���� $ ItemsPresenter��$ Cycle=� . # z $ Cycle=� P� " �@m�IsSuspendingPopupAnimation . . ��� $  
PART_Popup�� � ��$
 None=�P�  Z�  �@m�Icon. ��� $
  Icon�� � y�$ 	Collapsed=�P�  �@m�	IsChecked . . ��� $  
GlyphPanel�� � y�$ Visible=��� $
  Icon�� � y�$ 	Collapsed=�P�  �@m�IsHighlighted . . ��� $  templateRoot�� � �� �	#3D26A0DA�� $  templateRoot�� � �� �	#FF26A0DAP�  � {� .  . ��� $  templateRoot�� � �� �	#FF707070�� $  
GlyphPanel�� � �� �	#FF707070P� $! SubMenuScrollViewer�� � �� .  . ��� $  
OpaqueRect�� �� *VerticalOffset+$ SubMenuScrollViewer�� �@��Top�� $  
OpaqueRect�� �� *HorizontalOffset+$ SubMenuScrollViewer�� �@��Left�� " m�.R��� $ templateRoot��#��z��#��z��#��z�� . ._�� $
 Grid��$��-1q�	 �� $��22��$" MenuItemIconColumnGroup��$
��Auto ��� $��13 ��� $��* ��� $��30 ��� $" MenuItemIGTColumnGroup��$
��Auto ��� $��20 �.+��� $

 Icon��#��z $
��Icon��$��Center=�$��16��$��3q�# z $��Center=�$��16���� $ 
GlyphPanel�����	#FF26A0DA$��1q����	#3D26A0DA$��22��$��-1,0,0,0q�$y�Hidden=�$��Center=�$��22��._�F� $ Glyph��T������� �O �`�G �ڊ � �D �ڊ �    �XO � � �g5 ���A  ] ������� @    р�� �O ���	#FF212121$��LeftToRight=�$��11��$��9���� $
 ContentPresenter��#��z��#��z��$��2��# z $��Header��$
��Left=�#��z����. # z $��Center=��� $# 	TextBlock��$��4��#��z��$	$ 0.7Q�#��z% $��Center=�F� $ 
RightArrow��$��5��"����        � 	= �g5 �    ��j 	#���  $
��Left=�$��10,0,0,0q�$��Center=�� $ 
PART_Popup�� . |�.  $& -2��#��� -�$��Right=�$' -3������ *IsSubmenuOpen+ �� *TemplatedParent+.���� $ SubMenuBorder�����	#FF999999$��1q�#���  $ 2q�._��� $ SubMenuScrollViewer����C� *�� $ MenuScrollViewer��" �+.��� $ Grid1��$ Enabled=�.+��� $ Canvas��$
��Left=�$��0��$	��Top=�$��0��.+��� $ 
OpaqueRect������ *
Background+$ SubMenuBorder������ *ActualHeight+$ SubMenuBorder������ *ActualWidth+$ SubMenuBorder���� $ 	Rectangle��#��� ��$
��Left=�$��29,2,0,2q�$��1���� $ ItemsPresenter��$ Cycle=� . # z $ Cycle=� P� " �@m�IsSuspendingPopupAnimation . . ��� $  
PART_Popup�� � ��$
 None=�P�  Z�  �@m�Icon. ��� $
  Icon�� � y�$ 	Collapsed=�P�  �@m�	IsChecked . . ��� $  
GlyphPanel�� � y�$ Visible=��� $
  Icon�� � y�$ 	Collapsed=�P�  �@m�IsHighlighted . . ��� $  templateRoot�� � ��
 ���� �� $  templateRoot�� � �� �	#FF26A0DAP�  � {� .  . ��� $  templateRoot�� � �� �	#FF707070�� $  Glyph�� � �� �	#FF707070�� $  
RightArrow�� � �� �	#FF707070P� $! SubMenuScrollViewer�� � �� .  . ��� $  
OpaqueRect�� �� *VerticalOffset+$ SubMenuScrollViewer�� �@��Top�� $  
OpaqueRect�� �� *HorizontalOffset+$ SubMenuScrollViewer�� �@��Left