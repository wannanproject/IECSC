   M S B A M L   `   `   `  ����P  LIECSC.RFID_ORACLE_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:IECSC.RFID_ORACLE_WPFIECSC.RFID_ORACLE_WPF  5   	   Y1clr-namespace:IECSC.RFID_ORACLE_WPF.CustomControl#IECSC.RFID_ORACLE_WPF.CustomControl  5   	   &     IECSC.RFID_ORACLE_WPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:IECSC.RFID_ORACLE_WPF     5   	   KCustomControl1clr-namespace:IECSC.RFID_ORACLE_WPF.CustomControl     5   	     � Title$  
RFID读取��5	   	   $	��749��6   $
��1256��6%    � Icon$ RFID_ORACLE_WPF.ico��62   .��5
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
��Auto��6!   $��Socket连接状态:��6/   $��微软雅黑)�6E   $��12(�6W   �� 5      -   5        $ recPlcConnStatus��5      $��25��62   $��15��6=   
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
��Auto=�65   .��5      � -   5         �Name$ GridLocList��5       $
��Auto��65   $
��Auto��6B   5       5!      5A      5B      5C      