   M S B A M L   `   `   `  ����  Lab4    Lab4.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Horse Racing��5   	   $	��600��6   $	��800��6*   .��5      � .+�5   
   � $��1��6    � RowDefinitions 5      �� 5      $	��960��6    �� 5	      $	��210 �6    .+�5      � -   5         �Name$ 	FieldGrid��5       $��0��6   .+�5      �� -   5         ��Name$ RenderField��5      �� 5       �� Orientation$ 
Horizontal=�6   $��Bottom=�66   $��Center=�6Q   $��0 0 0 15q�6n   .+�5      �� -   5         ��Name$ PlayersCounter��5   5   $��50��5      $��40��6*   
�������
������� �� SelectedIndex$ 1��5   #   .@�5      �� .��5      �� $��4��6(   $��20(�61   $��MV Boli)�6?   
���iii�6T   5      �� 5      .��5      �� $��5��6(   $��20(�61   $��MV Boli)�6?   
���iii�6T   5      �� 5      .��5      �� $��6��6(   $��20(�61   $��MV Boli)�6?   
���iii�6T   5      5      �� 5      -   5         ��Name$ StartButton��5   w   $	��150��6   $��40��6)   
������ 
���iii�
�������$��10 0 0 0q�6�   .��5      �� -   5         ��Name$ StartButton_TextBlock��5   i   $��Start��6$   $��20(�61   $��MV Boli)�6?   
���iii�6i   5      �� 5      -   5        $ ChangeFocusHorse��5   F   $��10 0 0 0q�6   $��40��6/   $��40��6:   
������ 
���iii�
������ 6�   5      � 5      -   5        	 �Name$	 SelectedHorse_ProgressBar��5   l   $��20��6   $	��260��6*   $	��Top=�66   $
��0 15q�6N   
 � Maximum$
 100.0Q�6\   
������ 
���iii�
�������6�   �� 5       -   5        $% SelectedHorse_Percent_TextBlock��5    �   $��20��6   $	��Top=�6(   $
��0 15q�6@   $��Center=�6N    �� TextAlignment$ Center=�6k   
���   �$
��0.0%��6�   $��17(�6�   $��SimSun)�6�   5!      &   System.Windows.Controls.DataGrid  5"      -	   5          Name$ PlayersList��5"   *   
�������   CanUserSortColumns .     AutoGenerateColumns .  $��1��6v      	RowHeight$ 35Q�6�   ��6�   ��  �� Path$ .� �� Mode$ OneWay=� �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�   Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6B   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *Name+6B     5%      $ Position��6F    6)   �� *
Position+6F     5&      $
 Time��6K    6)   �� *FormattedTime+6K   5(      5)      5*      5+      