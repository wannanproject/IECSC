   M S B A M L   `   `   `  ����H  DWCS.ExcuteJob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WCS.ExcuteJobWCS.ExcuteJob  5   	       WCS.ExcuteJob.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WCS.ExcuteJob     5   	     � Title$  作业执行��5   	   $��588.99��6   $��999.785��6&    � 	Resources 5	      -   'WCS.ExcuteJob.JobStatusToBrushConverter   JobStatusToBrushConverter��%   &	          5
   
   6*   5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��5* �6   �� 5      $��4* �6    � ColumnDefinitions 5   
   �� 5      $
��635* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgMain��5      $��Stretch=�6#   $��Stretch=�6A   $��5q�6]      CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .     SelectionMode$ Single=�5   `   $��1��6w   	   HorizontalGridLinesBrush#	 � ��6�   
   VerticalGridLinesBrush#
 � ��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$ 作业编号��6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *JobNo+6R     5       . $ 作业名称��6S    6%   �� *	JobName+6S     5       . $ 执行间隔��6T    6%   �� *
Interval+6T     5       . $ 作业状态��6y    6%   �� *	JobStatus+ �� UpdateSourceTrigger$ PropertyChanged=�   ElementStyle 5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � �� �� Value 6;   �� *	JobStatus+ �� 	Converter# [  5      5!        5"       . $ 是否启用��6T    6%   �� *
UsedFlag+6T     5#       . $ 执行次数��6{    6%   �� *ExcuteCount+$ PropertyChanged=�6{     5$       . $ 总执行时长��6    6%   �� *TotalExcuteTime+$ PropertyChanged=�6     5%       . $ 最长执行时长(ms)��6}    6%   �� *MaxExcuteTime+$ PropertyChanged=�6}     5&       . $ 最短执行时长(ms)��6}    6%   �� *MinExcuteTime+$ PropertyChanged=�6}     5'       . $ 平均执行时长(ms)��6}    6%   �� *AvgExcuteTime+$ PropertyChanged=�6}     5(       . $ 最近一次执行时间��6�    6%   �� *LastExcuteTime+$ PropertyChanged=� �� StringFormat$ {0:yyyy-MM-dd HH:mm:ss}��6�     5)       .    Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6|   $ 执行结果��6�    6%   �� *ExcuteResult+$ PropertyChanged=�6�   5+      �� 5,   
   -   5         ��Name$ 	txtRecord��5,      $��5q�6%   $��2��60    �� TextWrapping$
 Wrap=�6=   #��� ��6Q   " �� VerticalScrollBarVisibility$
 Auto=�6�   $ �� HorizontalScrollBarVisibility$
 Auto=�6�    �� 
IsReadOnly . $	��5,2q�6  �� 5-   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��36��6K   $��480,10,302,10q�6W   $��Center=�6n   $	��210��6�   ���	#FFE6DEDE.+�5.      �� $��等待��6   $��0q�6!   $��10,0,0,0q�6-   $��Center=�6?   $��Center=�6a   $��30��6�   $��30��6�   #��� ��6�   $��1q�6�   
�������6�   �� 5/      $��执行��6   $��0q�6!   $��10,2.5,0,2.5q�6-   $��Center=�6C   $��Center=�6e   $��30��6�   $��30��6�   #��� ��6�   $��1q�6�   
����ح�6  �� 50      $��成功��6   $��0q�6!   $��10,2.5,0,2.5q�6-   $��Center=�6C   $��Center=�6e   $��30��6�   $��30��6�   #��� ��6�   $��1q�6�   
������6  �� 51      $��失败��6   $��0q�6!   $��10,2.5,0,2.5q�6-   $��Center=�6C   $��Center=�6e   $��30��6�   $��30��6�   #��� ��6�   $��1q�6�   
���  ��6  �� 52      $��暂停��6   $��0q�6!   $��10,2.5,0,2.5q�6-   $��Center=�6C   $��Center=�6e   $��30��6�   $��30��6�   #��� ��6�   $��1q�6�   
��� ���6  54      �� 55   
   $ 
Horizontal=�6   $
��Left=�6.   $��40��6I   $��24,0,0,0q�6U   $��Center=�6g   $	��422��6�   .+�56      �� -   5         ��Name$ btnSuspendAllJob��56      $��全部暂停��6/   $
��Left=�6>   $��Center=�6Y   $��75��6t   $��30��6�   $��10,0,0,0q�6�   �� 57      -   5        $ btnContinueAllJob��57      $��全部继续��60   $
��Left=�6?   $��Center=�6Z   $��75��6u   $��30��6�   $��10,0,0,0q�6�   �� 58      -   5        $ btnSuspendJob��58      $��暂停选中作业��6,   $
��Left=�6=   $��Center=�6X   $��95��6s   $��30��6~   $��20,5,0,5q�6�   6�   �� 59      -   5        $ btnContinueJob��59      $��继续选中作业��6-   $
��Left=�6>   $��Center=�6Y   $��95��6t   $��30��6�   $��10,5,0,5q�6�   5:      5<      5=      