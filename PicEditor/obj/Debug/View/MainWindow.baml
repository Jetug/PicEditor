   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	   D @NDragDrop, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:NDragDrop;assembly=NDragDrop	NDragDrop 5	   	   D @PicEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:PicEditor.ViewPicEditor.View 5
   	   ;"clr-namespace:PicEditor.BehavioursPicEditor.Behaviours 5   	   9!clr-namespace:PicEditor.ViewModelPicEditor.ViewModel 5   	   F BPicControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:PicControls;assembly=PicControlsPicControls 5   	   ?$clr-namespace:PicEditor.UserControlsPicEditor.UserControls 5   	      PicEditor.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Z VDevExpress.Mvvm.UI, Version=19.2.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   a	 ]GongSolutions.WPF.DragDrop, Version=2.0.0.0, Culture=neutral, PublicKeyToken=91f1945125b7a587ddurn:gong-wpf-dragdrop 	 5   	   b
 ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1(j http://www.galasoft.ch/mvvmlight 
 5   	   4ndd*clr-namespace:NDragDrop;assembly=NDragDrop 
 5	   	   (localclr-namespace:PicEditor.View 
 5
   	   *b"clr-namespace:PicEditor.Behaviours 
 5   	   *vm!clr-namespace:PicEditor.ViewModel 
 5   	   8pic.clr-namespace:PicControls;assembly=PicControls 
 5   	   /pic1$clr-namespace:PicEditor.UserControls 
 5   	   -   5        $��window��5   	     � WindowStartupLocation$  CenterScreen=�5   	      WindowStyle#���   5   	    � Title$ 	PicEditor��5   	   $	��600��5   	   $	��690��5   	   $	��600��5   	   $	��690��5   	    � DataContext 5      &  PicEditor.ViewModel.MainWindowVM  5   
   .   (System.Windows.Interactivity.Interaction   Triggers 5"      /   )System.Windows.Interactivity.EventTrigger  5#   
      	EventName$ 	MouseMove��6   .   (System.Windows.Interactivity.TriggerBase   Actions. 5$      / 
 )GalaSoft.MvvmLight.Command.EventToCommand     PassEventArgsToCommand .    Command 6   �� *WindowMouseMove+5%      5&        5(   
   $ Loaded��6   . 5)      6   0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *WindowLoaded+5*        5,   
   $ MouseLeftButtonUp��6   . 5-         6$   �� *WindowLeftButtonUp+5.        50   
   $ 	MouseDown��6   . 51         .  6   �� *WindowMouseDown+52      53        55   
   $ Closing��6   . 56         6$   �� *WindowClosing+57        59   
   $ PreviewKeyDown��6   . 5:         .  6   �� *ClosePictureWithKey+5;      5<      .��5?      �   BackgroundBrush#���  6   	 � ColumnDefinitions	 5@   
   �� 5A      $��40 �6   �� 5B      .+�5E   
    � -   5        $��frame��5E      
  � NavigationUIVisibility$
 Visible=�5G      $��	2,0,10,10q�5H        
FrameStyle#���  5I      $��1��5J      ��5F      �� *	FramePage+5J      �� 5a   
   $
��Left=�5b      $��3,10,0,0q�5c      $	��Top=�5d      $��35��5e      $��35��5f      $��Center=�5g      $��Center=�5h        ButtonStyle#���  5i      $��0,0,0,3q�5j      ��5a      �� *
OpenFolder+.��5k      F� �������! -1� 	=  -1�,  -1�� `q#�� ��р� ��[ Ӏ� ��J ,  	=  	=  	= р��  	= � � ��[ ���!��[ �@o@ z ��� �� �@o@ z � 	=  z � 	= ���`�] ��� �(b��� ��]?�I�`�;�'��/ -1��! -1	  ForegroundBrush#���  5l      5m         PicControls.PictureControl  5o   
   -   5        $	��pic��5o         BackgroundOpacity$	 0.9Q�5p      $��2��5w      y�5q      �� *PictureVisibility+   ImageSource 5r      �� *PictureSource+   BGMouseLeftButtonDown 5s      �� *ClosePicture+   ImageHeight 5t      �� *ImageHeight+   
ImageWidth 5u      �� *
ImageWidth+ 5x        5y      $ MouseUp��6!   . 5z         6,   �� *ClosePictureWithKey+5{      5}      5~      5      