   M S B A M L   `   `   `  ����F  BPicControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PicControlsPicControls  5      "    PicControls.ThumbnailControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:PicControls     5      -   5        $��previewControl��5         PreviewControlTemplate#���   5        � 	Resources  5       �� 
TargetType ��Name �� Padding J� SnapsToDevicePixels �� ContentStringFormat �� ContentStringFormat �� Triggers P� Property	 P� Value
 �� 
TargetName  MouseOverBorderBrush �� Value �� Property��%�  &	        �� 5   
   " >�5      .R�5      �� $ border��6   #��z��5      #��z��5      #��z��5      # z��5       . ._�5      �� #��z��6#   #��z��5   #   # z 5   #   #��z��5   #   # z 5   #   #��z��5   #   5        5!      P� 5#       �@J�IsMouseOver	 . . �5%      �� $
 border��5&      # �  5'       � ��5(      5+      5,      .��5-      � .+�54   
   �� -   5        $��UniformToFill=�55      ��54      �� *ImageSource+ �� ElementName$ previewControl��57      �� 58   
   
���   � J� Opacity$
 0.75Q�59      $��20��5;      $��Center=�5<      $��Bottom=�5=      $��0,0,0,0q�5>      ��5:      �� $ previewControl�� �� Path$ Width�5>      �� 5?   
   ���	#FF056EB6$��12(�5A      $��20��5C      $��Center=�5D      $��Bottom=�5E      $��5,1,5,0q�5F      $��0,0,0,0q�5G      $��Center=�5H      ��5?      �� *	ImageName+$ previewControl����5B      �� $ previewControl��$ Width�5H      �� 5L   
   -   5        $��	selection��5L      $
��Left=�5N      $	��Top=�5O      $��15��5P      $��15��5Q      ���	#FF252525
����������	#FF575757 J� OpacityMask
 �   �  CheckBoxStyle#���  5Y      y�5M      �� *SelectionVisibility+$ previewControl�� d� 	IsChecked 5T      �� *
IsSelected+$ previewControl�� � LayoutTransform 5Z      �� 5[       �� ScaleX$	 1.5Q�6!    �� ScaleY$	 1.5Q�5\   !   5^      5a      5c      