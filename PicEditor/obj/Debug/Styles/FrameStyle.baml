   M S B A M L   `   `   `  ����D  @PicEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PicEditor.StylesPicEditor.Styles  5      b ^PresentationFramework.Aero2, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35hKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2Microsoft.Windows.Themes 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Z VDevExpress.Mvvm.UI, Version=19.2.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 $   JournalEntryUnifiedViewConverter  CurrentEntryBackground  �� Color �� Opacity 5 1NavigationWindowNavigationButtonJournalEntryStyle �� 
TargetType �� Property �� Value �� RelativeSource �� AncestorType �� 
TargetType �Name	 J� SnapsToDevicePixels
 F�Name �� StrokeEndLineCap �� StrokeStartLineCap �� Triggers P� Property P� Value �� 
TargetName ]� 
Conditions �� Property �� Value ! NavigationWindowDownArrowFill �� EndPoint �� 
StartPoint � Color � Offset  NavigationWindowMenuItem �Name � AllowsTransparency � VerticalOffset5   /Microsoft.Windows.Themes.SystemDropShadowChrome   Name    Color ��Name ��Name �� 
ResourceId  �� TypeInTargetAssembly! �� ClearTypeHint" ��Name# �� ElementName$ ��Name-  'System.Windows.Input.KeyboardNavigation%   DirectionalNavigation&   TabNavigation' �� CornerRadius( �� StrokeLineJoin) P� 
SourceName  NavigationWindowMenu  NavigationWindowButtonStyle  ButtonStyle* �� BasedOn # NavigationWindowBackButtonStyle 	 ForegroundBrush+ �� Triggers 
 DisabledForegroundBrush & "NavigationWindowForwardButtonStyle, J� RenderTransformOrigin- �� ScaleX  FrameNavChromeTemplateKey. �� Padding/ � ColumnDefinitions0 ��Name  
FrameStyle�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      (localclr-namespace:PicEditor.Styles  5      VThemesKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2  5      %!  &	        &	      7��&	 :     7  &	 q    &	 -	    7  &	 �    &	 c    7  &	     7  &	 �    7  7 	 &	     7  7  &	 %    7  7  �� 5      6'   �� 5      8    $
 0.25Q�5	      �� 5      " m�5      .�5   
   ��  �@�OverridesDefaultStyle . 5      �� 5   
    � �� 5      �� *(JournalEntry.Name)+�� 5   
    �@m�Command$( "NavigationCommands.NavigateJournal��5      �� 5   
    �@m�CommandTarget 5      �� *TemplatedParent+ �� " o��� 5   
    �@m�CommandParameter 5      ��  �� *Self+�� 5   
    �@��JournalEntryPosition 5      �� *97(JournalEntryUnifiedViewConverter.JournalEntryPosition)+�� 5   
    � �� 5      �� 5      " m�6"   .R�5      � $ Panel��6   	 . 
������ .+�5       F� $
 Glyph��6   $��10��5!      $��2��5"      #��z��5#      $ Triangle=�5$      	 .  $ Triangle=�5&      $	��7,5q�5'      $��10��5(      $
��Left=�5)      �� 5*      $��Header��6+   $��	24,5,21,5q�5+   +   5,       5-      P� 5.       �@��JournalEntryPosition$ Current=�5/   "   . �50      �� $ Panel��51   %   8    � ��52   %   �� 53      $ Glyph��54   %    � ��" ��    @KL Ѡ%&  z ���j ��- 	55   %   �� 56      $ Glyph��57   %    � ��$ LeftToRight=�58   %   �� 59      $ Glyph��5:   %    �@��StrokeLineJoin$ Miter=�5;   %   5<      P� 5=       �@m�IsHighlighted . . �5?      �� # � ��5@   %    � ���� 5A      $ Panel��5B   %   # � ��5C   %    � ��5D      ]� 5E       5F      �� 5G   "    �@m�IsHighlighted . 5H   ,   �� 5I   "    �@��JournalEntryPosition$ Forward=�5J   ,   ./�5L      �� $ Glyph��5M   %    � ��
 �����5N   %   �� 5O      $ Glyph��5P   %    � ��4 ��@B @KL ���j @KL �@KL @B �@T� @KL �@KL @T� 	5Q   %   5R      ]� 5S       5T      �� 5U   "    �@m�IsHighlighted . 5V   ,   �� 5W   "    �@��JournalEntryPosition$
 Back=�5X   ,   ./�5Z      �� $ Glyph��5[   %    � ��
 �����5\   %   �� 5]      $ Glyph��5^   %    � ��4 ��@T� @KL ���- @KL �@KL @B �@B @KL �@KL @T� 	5_   %   5`      5b      5d      5e      �� 5i      $	 0,1&�5j      $	 0,0&�5k      ��5m      � � 5n      $ #33B5DA��6   $ 0Q�5o      � 5p      $ #335799��6   $ 1Q�5q      5r      5t      �� 5x      " m�5y      .�5z   
   ��  �@�OverridesDefaultStyle . 5{      �� 5|   
    �@��PanningMode$
 Both=�5}      �� 5~   
    �@��IsFlicksEnabled .  5      �� 5�   
    � �� 5�      �� 5�      " m�6"   .R�5�      � .+�5�      � $ 
PART_Popup��6     . |�.  #��� -�5�       $��Bottom=�5�       $ 2��5�       ��5�       �� *(MenuItem.IsSubmenuOpen)+ �� *TemplatedParent+.��5�         $
 Shdw��6<   $ Transparent��5�   <   ._�5�   "   �� $ SubMenuBorder��6)   #��� ��5�   )   $��1q�5�   )   #��� ��5�   )   ._�5�   &   �� $ SubMenuScrollViewer��63   ��5�   3   C� *�� $ MenuScrollViewer��"  �+.��5�   *   � $! Enabled=�6/   .+�5�   .   �� $
��Left=�65   $��0��5�   5   $	��Top=�5�   5   $��0��5�   5   .+�5�   2   �� $" 
OpaqueRect��6<   ��5�   <   �� *
Background+$# SubMenuBorder����5�   <   �� *ActualHeight+$# SubMenuBorder����5�   <   �� *ActualWidth+$# SubMenuBorder��5�   /   �� 5�   .   $$ ItemsPresenter��6=   $% Cycle=�5�   =   $��2q�5�   =   $& Cycle=�5�   =   5�   +   5�   '   5�   #   5�      5�      � 5�      $ Panel��6   
������ $��Right=�5�      $��26��5�      .+�5�      �� $ HighlightBorder��6%   ���	#B0B5BACE$��1,1,1,1q�5�   %   $' 2��5�   %   	 . $y�Hidden=�5�   %   ��5�   "   �� 5�   &   $	 0,1&�6:   $	 0,0&�5�   :   .��5�   *   � $ 	#20DDDDDD��67   $ 0Q�5�   7   � 5�   *   $ 	#80FFFFFF��67   $ 1Q�5�   7   5�   '   ._�5�   "   �� ���	#A0FFFFFF$��1q�5�   )   $	' 1.5��5�   )   5�      F� 5�      $
 Arrow��6#   "����        � �D @KL �@T�     	8��  $��Right=�5�   #   #��z��5�   #   	 .  
�������$��1��5�   #   $( Round=�5�   #   $��Center=�5�   #   5�      5�       5�      P� 5�       �@m�IsHighlighted . . �5�      �� $ HighlightBorder��5�   %    � y�$ Visible=�5�   %   5�      P� 5�       � {� .  . �5�      �� $ Arrow��5�   %    � �� �#A5AABE5�   %   5�      P� 5�      $) 
PART_Popup��5�   "    �@�HasDropShadow . . �5�      �� $
 Shdw��5�   %    � ��$ 0,0,5,5q�5�   %   �� 5�      $
 Shdw��5�   %    �@J�SnapsToDevicePixels . 5�   %   �� 5�      $
 Shdw��5�   %    �@  Color$ 	#71000000��5�   %   5�      P� 5�      $) SubMenuScrollViewer��5�   "    � �� .  . �5�      �� $ 
OpaqueRect��5�   %    5�   %   �� *VerticalOffset+$# SubMenuScrollViewer�� �@��Top�� 5�      $ 
OpaqueRect��5�   %    5�   %   �� *HorizontalOffset+$# SubMenuScrollViewer�� �@��Left5�      5�      5�      5�      �� 5�      " o�5�      .�5�   
   ��  � �� 5�      �� 5�      .R�5�      ]� 5�      5�      �� 5�   
    �@�OverridesDefaultStyle . 5�      �� 5   
    �@ TabNavigation$
 None=�5     �� 5  
    �@o�
IsMainMenu .  5     �� 5  
    � �� 5     �� 5     " o�6"   .R�5     �� 5     5
     5     �� 5O     " ��5P     8*   .�5R  
   ��  �@�OverridesDefaultStyle . 5S     �� 5T  
    � |� .  5U     �� 5V  
    � ��$ 24��5W     5X     �� 5\     " ��5]     8*   .�5_  
   ��  � ��$# NavigationCommands.BrowseBack��5`     �� 5a  
    � �� 5b     F� 5c     $
 Arrow��6   �����P� Wu ���W ��� � �� �9� `� `� Ѡz�  �� � [� ��� �@�� ��Y `� `� ��Ȁ �Z QpQ� ��Q�@�0YX  `� `� 	#��� 	 5e     $��Center=�5f     $��Center=�5g     5i     + 5j  
   P� 5k      � {� .  . �5m     ��  � �� 5n     F� 5o     �����P� Wu ���W ��� � �� �9� `� `� Ѡz�  �� � [� ��� �@�� ��Y `� `� ��Ȁ �Z QpQ� ��Q�@�0YX  `� `� 	#��� 
 5q     $��Center=�5r     $��Center=�5s     5u     5v     5x     �� 5|     " ��5}     8*   .�5  
   ��  � ��$&  NavigationCommands.BrowseForward��5�     �� 5�  
    � �� 5�     F� 5�     $
 Arrow��6   $��0��5�     �����P� Wu ���W ��� � �� �9� `� `� Ѡz�  �� � [� ��� �@�� ��Y `� `� ��Ȁ �Z QpQ� ��Q�@�0YX  `� `� 	8�� $��Center=�5�     $, 0.5,0&�5�     $��Center=�5�     z�5�     �� 5�     $- -1Q�6)   5�     5�     + 5�  
   P� 5�      � {� .  . �5�     ��  � �� 5�     F� 5�     $��0��6   �����P� Wu ���W ��� � �� �9� `� `� Ѡz�  �� � [� ��� �@�� ��Y `� `� ��Ȁ �Z QpQ� ��Q�@�0YX  `� `� 	#��� 
 5�     $��Center=�5�     $, 0.5,0&�5�     $��Center=�5�     z�5�     �� 5�  "   $- -1Q�61   5�     5�     5�     5�     �� 5�     "  �5�     .R�5�  
   �� #��z��6   #��z��5�     #��z��5�     #. z��5�     ._�5�     ]� .+�5�     � 
������ $	��Top=�5�     $��24��5�     / 5�     �� 5�     $
��Auto �6+   �� 5�     $
��Auto �6+   �� 5�     $��16 �6+   �� 5�     $��* �6+   .+�5�     �� $��0��6   $��0,0,0,0q�5�     8��  5�     �� 5�     $��1��6   $��0,0,0,0q�5�     8�� 5�     5�     �� 5�     $0 PART_FrameCP��6#   5�     5�      5�  
   5�     �� 5�     "  �5�     .�5�  
   ��  � �� 5�     �� 5�     "  �6"   .R�5�     �� #��z��6   #��z��5�     #��z��5�     #. z��5�     ._�5�     �� $0 PART_FrameCP��6+   5�     5�     5�     + 5�  
   P� 5�      �@ �NavigationUIVisibility$ Visible=�5�     . �5�     ��  � ��8   5�     5�     ]� 5�      5�     �� 5�      �@ �JournalOwnership$ OwnsJournal=�5�      �� 5�      �@ �NavigationUIVisibility$ 	Automatic=�5�      ./�5�     ��  � ��8  5�     5�     5�     5�     