   M S B A M L   `   `   `  ����F  BIECSC.TRANS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:IECSC.TRANS.CustomControlIECSC.TRANS.CustomControl  5      *    $IECSC.TRANS.CustomControl.LocControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:IECSC.TRANS.CustomControl     5      .��5	      �� -   5          ��Name$  gbTitle��5	      $�� ��6    $
��Auto��6*   $
��Auto��67   $��3q�6E   .��5
   
   � $
��Auto��6   $
��Auto��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��45 �6#   .+�5      � $��1��6   $��45��6#   $
��Auto��6.   $��Right=�6<    5      �� 5      $��1* �6'   �� 5      $��2* �6'    � RowDefinitions 5      �� 5      $	��10* �6$   �� 5      $	��15* �6$   �� 5      $	��15* �6$   �� 5      $	��15* �6$   �� 5      $	��15* �6$   �� 5      $	��15* �6$   �� 5      $	��15* �6$   .+�5      A� -   5         A�Name$ epAuto��5      $��12��6*   $��12��65   $��1��6A   $��0��6N   $��Center=�6^   $��Center=�6{   ���	#FF869FF16�   �� 5       $
��Auto��6   $
��Auto��6%   $��1��63   $��1��6@   $��自动��6P   $��10(�6]   $
��Left=�6k   $��Center=�6�   A� 5!      -   5        $ epFault��5!      $��12��6+   $��12��66   $��2��6B   $��0��6O   $��Center=�6_   $��Center=�6|   ���	#FF869FF16�   �� 5"      $
��Auto��6   $
��Auto��6%   $��2��63   $��1��6@   $��故障��6P   $��10(�6]   $
��Left=�6k   $��Center=�6�   A� 5#      -   5        $ 	epLoading��5#      $��12��6-   $��12��68   $��3��6D   $��0��6Q   $��Center=�6a   $��Center=�6~   ���	#FF869FF16�   �� 5$      $
��Auto��6   $
��Auto��6%   $��3��63   $��1��6@   $��有载��6P   $��10(�6]   $
��Left=�6k   $��Center=�6�   A� 5%      -   5        $ 	epRequest��5%      $��12��6-   $��12��68   $��4��6D   $��0��6Q   $��Center=�6a   $��Center=�6~   ���	#FF869FF16�   �� 5&      $
��Auto��6   $
��Auto��6%   $��4��63   $��1��6@   $��请求��6P   $��10(�6]   $
��Left=�6k   $��Center=�6�   A� 5'      -   5        $ epFree��5'      $��12��6*   $��12��65   $��5��6A   $��0��6N   $��Center=�6^   $��Center=�6{   ���	#FF869FF16�   �� 5(      $
��Auto��6   $
��Auto��6%   $��5��63   $��1��6@   $��空闲��6P   $��10(�6]   $
��Left=�6k   $��Center=�6�   A� 5)      -   5        $ epToLoad��5)      $��12��6,   $��12��67   $��6��6C   $��0��6P   $��Center=�6`   $��Center=�6}   ���	#FF869FF16�   �� 5*      $
��Auto��6   $
��Auto��6%   $��6��63   $��1��6@   $��取货��6P   $��10(�6]   $
��Left=�6k   $��Center=�6�   5+      � 5-      $
��Auto��6   $
��Auto��6     5.      �� 5/      $��40 �6'   �� 50      $��* �6'    52      �� 53      $	��15* �6$   �� 54      $	��10* �6$   �� 55      $	��10* �6$   �� 56      $	��10* �6$   �� 57      $	��10* �6$   .+�59      �� -   5         ��Name$ btnLocPlcNo��59      $��A1101��6.   $��0��6>   $��0��6K   $��2��6[   $��2q�6o   $
��Auto��6z   $
��Auto��6�   $
��Bold"�6�   $��18(�6�   ���#A1C2DC � Cursor$
 Hand��6�   ��6�   Z� 6�   �� 5:      $
��Auto��6   $
��Auto��6%   $��1��63   $��0��6@   $��	任务号��6P   $��10(�6^   $��Center=�6l   $��Center=�6�   
���   �6�   �� 5;      -	   5         ��Name$ tbTaskNo��5;      $��1��6,   $��1��6<   $��1q�6I   $
��Auto��6T   $
��Auto��6a    �� 
IsReadOnly . ���	#FFE9EAE8 �� TextAlignment$
 Left=�6�   $��Center=�6�   ��6�   Z� �� 5<      $
��Auto��6   $
��Auto��6%   $��2��63   $��0��6@   $��	工装号��6P   $��10(�6^   $��Center=�6l   $��Center=�6�   �� 5=      -
   5        $ 
tbPalletNo��5=      $��1��6.   $��2��6>   $��1q�6K   $
��Auto��6V   $
��Auto��6c    . ���	#FFE9EAE8$
 Left=�6�   $��Center=�6�   ��6�   Z� �� 5>      $
��Auto��6   $
��Auto��6%   $��3��63   $��0��6@   $��	起始点��6P   $��10(�6^   $��Center=�6l   $��Center=�6�   
���   �6�   �� 5?      -   5        $ tbSlocNo��5?      $��1��6,   $��3��6<   $��1q�6I   $
��Auto��6T   $
��Auto��6a    . ���	#FFE9EAE8$
 Left=�6�   $��Center=�6�   ��6�   Z� �� 5@      $
��Auto��6   $
��Auto��6%   $��4��63   $��0��6@   $��	目的点��6P   $��10(�6^   $��Center=�6l   $��Center=�6�   �� 5A      -   5        $ tbElocNo��5A      $��1��6,   $��4��6<   $��1q�6I   $
��Auto��6T   $
��Auto��6a    . ���	#FFE9EAE8$
 Left=�6�   $��Center=�6�   ��6�   Z� 5B      5C      5D      5E      