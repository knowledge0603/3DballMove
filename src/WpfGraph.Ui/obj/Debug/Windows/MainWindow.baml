   M S B A M L   `   `   `  ����P  LPalmmedia.WpfGraph.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Palmmedia.WpfGraph.UI.ViewModels Palmmedia.WpfGraph.UI.ViewModels  5      Q-clr-namespace:Palmmedia.WpfGraph.UI.ResourcesPalmmedia.WpfGraph.UI.Resources  5      ]3clr-namespace:Palmmedia.WpfGraph.UI.ViewModels.Menu%Palmmedia.WpfGraph.UI.ViewModels.Menu  5      I)clr-namespace:Palmmedia.WpfGraph.UI.ViewsPalmmedia.WpfGraph.UI.Views  5      .    (Palmmedia.WpfGraph.UI.Windows.MainWindow   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    8x,http://schemas.microsoft.com/winfx/2006/xaml    5      O KWPFLocalizeExtension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;lex1http://schemas.root-project.org/xaml/presentation  5      7vm.clr-namespace:Palmmedia.WpfGraph.UI.ViewModels  5      7res-clr-namespace:Palmmedia.WpfGraph.UI.Resources  5      @vmMenu3clr-namespace:Palmmedia.WpfGraph.UI.ViewModels.Menu  5      5views)clr-namespace:Palmmedia.WpfGraph.UI.Views  5      :d2http://schemas.microsoft.com/expression/blend/2008  5	      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  6B     � Title$  浮球模型��5
      $	��600��6   $	��800��6   $	��600��6<   $	��800��6L    � Icon$ ../ProgrammIcon.ico��6[    � 	Resources 5      B  <Palmmedia.WpfGraph.UI.Resources.GraphElementTemplateSelector    GraphElementTemplateSelector  NodeDataTemplate.   (Palmmedia.WpfGraph.UI.Views.NodeDataView  EdgeDataTemplate.   (Palmmedia.WpfGraph.UI.Views.EdgeDataView S� 
MethodName S� 
ObjectTypeA  ;Palmmedia.WpfGraph.UI.ViewModels.Menu.AlgorithmsMenuBuilder  AlgorithmMenuItems��%�   &	        &	      &	 ;     &	 c       5   
   6+   �� 5   
   .R�5        5      �� 5   
   .R�5        5      S� 5   
   $ GetMenuItems��6   "  67   6j   5      .��5      �  � RowDefinitions 5   
   �� 5      $��22 �6   �� 5       � ColumnDefinitions 5      �� 5      �� 5      $	��180 �6   .+�5"   
   o� $��2��6   .@�5#      m� ��6   6  0WPFLocalizeExtension.Extensions.LocTextExtension  *Graph+�  �WPFLocalizeExtension.BaseExtensions.BaseLocalizeExtension`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Assembly$ Palmmedia.WpfGraph.UI��.@�5$      m� ��6     *Load+$ Palmmedia.WpfGraph.UI�� m� Command 6W   �� *LoadGraphCommand+	 �� Mode$	 OneTime=�m� 5%      ��6     *Save+$ Palmmedia.WpfGraph.UI�� 6W   �� *SaveGraphCommand+$	 OneTime=��� 5&      m� 5'      ��6     *Exit+$ Palmmedia.WpfGraph.UI�� 6W   �� *ExitCommand+$	 OneTime=�5(      m� 5*      ��6     *
Algorithms+$ Palmmedia.WpfGraph.UI����6Y   �� 
 �� Source#
 [ $	 OneTime=� 5+       �� DataType=  7Palmmedia.WpfGraph.UI.ViewModels.Menu.MenuItemViewModel �� ItemsSource��%�   (��       5,   /   *, +)�� 6   "  6/    6\   �� *ChildMenuItems+$	 OneTime=�.R�5-      m� ��6#   �� *Header+$	 OneTime=� 6K   �� *	Command+$	 OneTime=�5.      5/      50      51      �� 53   
   -   5        $��0��53      $��1��6!   
������ 55      '� 57   
   -   5         '�Name$ 
viewport3D��57      $��0��6)   $��1��69    '� Camera 59      =� 5:      -   5        
camera�� � LookDirection$ 0,0,-1005�5=       =� FieldOfView$ 90Q�5>       � Position 5<      �� *GraphViewModel.CameraPosition+$	 OneWay=�5>      .x�5A      7  1System.Windows.Media.Media3D.ContainerUIElement3D  -   5        	container��5A   #   g� 5C       g� Content 5D      h� 5E      .��5F      t�  �� Color$ White��6+    t� 	Direction$ 	-1,-1,-105�69   t� 5G      $ White��6+   $ -1,-1,105�69   5H      5J      5L      �� 5N   
   ���	#FF9D9D9D$��4,0,-4,0q�6%   $��1��67   $��1��6G   
���   �6T   �� 5O   
   $��1��6   $��1��6"   $��8,100,10,10q�6/   $	��Top=�6D   $��Stretch=�6\   ��5P   
   �� *!GraphViewModel.SelectedElements+$	 OneWay=� �� SelectedItem 5Q   
   �� * GraphViewModel.SelectedElement+$	 OneWay=���5R   
   Z� ��6    Z� ��66   Z� ��5S   
   �� #
 [   5T       �� Color��%  (��       5U   "   * SystemColors.HighlightBrushKey+)(�� #     5V   "   *SystemColors.ControlBrushKey+)(�� F     5W   "   *$"SystemColors.HighlightTextBrushKey+)�� 5U      $ 	#FF9D9D9D��6T   �� 5V      $ 	#FF9D9D9D��6R   �� 5W      $ Black��6X   5X      5Y      �� 5Z   
   $��
10,10,10,0q�6   $	��Top=�6'   $��1��6{   $��1��6�   ���	#FFB7B7B7��6?     *Zoom+$ Palmmedia.WpfGraph.UI��.��5[      �� -   5         ��Name$ slider��5[      $	��0.1q�6    � Maximum$
 3000Q�6,    � Minimum$ 0Q�6a    � Value 6;   �� *GraphViewModel.Zoom+6a   5\      5^      5_      