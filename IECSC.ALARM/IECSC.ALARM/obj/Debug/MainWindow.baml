   M S B A M L   `   `   `  ����F  BIECSC.ALARM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:IECSC.ALARMIECSC.ALARM  5   	       IECSC.ALARM.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:IECSC.ALARM     5   	     � Title$  报警采集��5   	   $	��749��6   $
��1256��6#    � Icon$ 	Alarm.ico��61   .��5	      ]� $
��Auto��6   $
��Auto��6   ��. .+�5   
   �� $
��Auto��6   $��25��6"   
������� �� Orientation$ 
Horizontal=�6E   $��Bottom=�6^   .+�5      �� $
��Auto��6   $
��Auto��6!   $��数据库连接状态:��6/   $��微软雅黑)�6B   $��12(�6T   �� 5      -   5         ��Name$ recDbConnStatus��5      $��25��61   $��15��6<   
���  ��6H   �� 5      $
��Auto��6   $
��Auto��6!   $��设备连接状态:��6/   $��微软雅黑)�6A   $��12(�6S   �� 5      -   5        $ recPlcConnStatus��5      $��25��62   $��15��6=   
���  ��6I   �� 5      $��  登陆时间:��6   $��微软雅黑)�6&   $��12(�68   �� 5      -   5         ��Name$ lbTime��5      $��2018-11-11 11:59:59��6$   $��微软雅黑)�6B   $��12(�6T   5      � 5   
   $
��Auto��6   $
��Auto��6    � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5      $	��450 �6#   .+�5      �� $��报警状态监控��6   $��0��6'   $
��Auto��67   $
��Auto��6D   $��3q�6R   .��5      �� -   5        $��scrollViewer1��5      $
��Auto=�64   .��5      � -   5         �Name$ GridLocList��5       $
��Auto��65   $
��Auto��6B   5      5      �� 5      -   5         ��Name$ 	gbExecLog��5      $��运行日志��6*   $��1��68   $
��Auto��6H   $
��Auto��6U   $��3q�6c   .��5      �� -   5         ��Name$ txtLocRecord��5      $
��Auto��60   $
��Auto��6=   	 �� TextWrapping$
	 Wrap=�6K   $��5q�6_   #��� ��6k   "
 �� VerticalScrollBarVisibility$

 Auto=�6�   $ �� HorizontalScrollBarVisibility$
 Auto=�6�    �� 
IsReadOnly . $��2��6  5      5       5!      5"      