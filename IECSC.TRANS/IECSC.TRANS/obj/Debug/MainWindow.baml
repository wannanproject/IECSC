   M S B A M L   `   `   `  ����F  BIECSC.TRANS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:IECSC.TRANSIECSC.TRANS  5   	   E'clr-namespace:IECSC.TRANS.CustomControlIECSC.TRANS.CustomControl  5   	       IECSC.TRANS.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:IECSC.TRANS     5   	   ACustomControl'clr-namespace:IECSC.TRANS.CustomControl     5   	     � Title$  	输送线��5	   	   $	��749��6   $
��1256��6"    � Icon$ 	Trans.ico��6/   .��5
      ]� $
��Auto��6   $
��Auto��6   ��. .+�5   
   �� $
��Auto��6   $��25��6"   
������� �� Orientation$ 
Horizontal=�6E   $��Bottom=�6^   .+�5      �� $
��Auto��6   $
��Auto��6!   $��数据库连接状态:��6/   $��微软雅黑)�6B   $��12(�6T   �� 5      -   5         ��Name$ recDbConnStatus��5      $��25��61   $��15��6<   
���  ��6H   �� 5      $
��Auto��6   $
��Auto��6!   $��设备连接状态:��6/   $��微软雅黑)�6A   $��12(�6S   �� 5      -   5        $ recPlcConnStatus��5      $��25��62   $��15��6=   
���  ��6I   �� 5      $��  登陆时间:��6   $��微软雅黑)�6&   $��12(�68   �� 5      -   5         ��Name$ lbTime��5      $��2018-11-11 11:59:59��6$   $��微软雅黑)�6B   $��12(�6T   5      �� 5   
   -   5         ��Name$ 	gbExecLog��5      $��运行日志��6&   $	��350��64   $
��Auto��6@   $��3q�6N   $��Right=�6Y   .��5      �� -   5         ��Name$ txtLocRecord��5      $
��Auto��6,   $
��Auto��69    �� TextWrapping$
 Wrap=�6G   $��5q�6[   #��� ��6g   " �� VerticalScrollBarVisibility$
 Auto=�6�   $	 �� HorizontalScrollBarVisibility$
	 Auto=�6�   
 �� 
IsReadOnly
 . $��2��6  5      � 5   
   $
��Auto��6   $
��Auto��6    � RowDefinitions 5      �� 5      $	��75* �6    �� 5      $	��25* �6    �� 5      $��30 �6    .+�5      �� $��站台状态监控��6   $��0��6'   $��0��67   $
��Auto��6D   $
��Auto��6Q   $��3q�6_   .��5      �� -   5        $��scrollViewer1��5      $
��Auto=�64   $
��Auto=�6Y   .��5      A� -   5         A�Name$ GridLocList��5   "   $
��Auto��67   $
��Auto��6D   5       5!      �� 5"      -   5        $ 	gbTaskCmd��5"      $��当前任务信息��6*   $��0��6:   $��1��6J   $
��Auto��6W   $
��Auto��6d   $��3q�6r   .��5#      &   System.Windows.Controls.DataGrid  -	   5          Name$	 dgv��5#      $��3q�6(      CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .     SelectionMode$ Single=�6�      HorizontalGridLinesBrush# � ��5$         VerticalGridLinesBrush# � ��6z      Columns 5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$ 	指令号��6}      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *ObjId+ �� UpdateSourceTrigger$ PropertyChanged=�6�     5'       . $ 	任务号��6~   $ 80 6�    6-   �� *TaskNo+$ PropertyChanged=�6�     5(       . $ 任务类型��6   $ 80 6�    6-   �� *	CmdType+$ PropertyChanged=�6�     5)       . $ 任务步骤��6   $ 80 6�    6-   �� *	CmdStep+$ PropertyChanged=�6�     5*       . $ 起始站台��6�   $	 100 6�    6-   �� *	SlocPlcNo+$ PropertyChanged=�6�     5+       . $ 目的站台��6�   $	 100 6�    6-   �� *	ElocPlcNo+$ PropertyChanged=�6�     5,       . $ 	托盘号��6�   $	 100 6�    6-   �� *
PalletNo+$ PropertyChanged=�6�     5-       . $ 起始站台类型��6�   $ 90 6�    6-   �� *
SlocType+$ PropertyChanged=�6�     5.       . $ 目的站台类型��6�   $ 90 6�    6-   �� *
ElocType+$ PropertyChanged=�6�     5/       . $ 站台编号��6~   $ 90 6�      
Visibility$ Hidden=�6�    6-   �� *SlocNo+$ PropertyChanged=�6�     50       . $ 站台编号��6~   $ 90 6�   $ Hidden=�6�    6-   �� *ElocNo+$ PropertyChanged=�6�   52      53      � 54      $��0��6   $��2��6#   $
��Auto��60   $
��Auto��6=   $��0,0,10,0q�6K    � ColumnDefinitions 55      �� 56      $
��100* �6'   �� 57      $��80 �6'   �� 58      $��80 �6'   �� 59      $��80 �6'   �� 5:      $��80 �6'   .+�5<      �� -
   5         ��Name$ 
btnShowAll��5<      $��0��6-   $��1��6:   $��3,0,3,0q�6J   $��界面刷新��6[   ���	#FFEDF0F0 � Cursor$
 Hand��6�   6�   �� 5=      -   5        $ 
btnRefSend��5=      $��0��6-   $��2��6:   $��3,0,3,0q�6J   $��任务重发��6[   ���	#FFEDF0F0$
 Hand��6�   6�   �� 5>      -   5        $ 	btnFinish��5>      $��0��6-   $��3��6:   $��3,0,3,0q�6J   $��强制完成��6[   ���	#FFEDF0F0$
 Hand��6�   6�   �� 5?      -   5        $ btnDel��5?      $��0��6-   $��4��6:   $��3,0,3,0q�6J   $��强制删除��6[   ���	#FFEDF0F0$
 Hand��6�   6�   5@      5A      5B      5C      