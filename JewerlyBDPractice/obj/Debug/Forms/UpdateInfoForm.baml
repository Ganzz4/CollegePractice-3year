   M S B A M L   `   `   `  ����@  <Lab11, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab11Lab11  5   	       Lab11.UpdateInfoForm   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   localclr-namespace:Lab11  5   	     � Icon$  /Icons/UpdateFormIcon.png��5	   	    � Title$/ )Обновление информации��5
   	   $
��1010��5   	   $
��1800��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�5   	   .��5      � ���#e0e1dd.+�5   
   �� $	��100��6   $	��Top=�6    �� CornerRadius$ 	0 0 120 0��66   ��5      �� 5       �� 
StartPoint$ 0.1,0&�6&    �� EndPoint$ 0.9,1&�69   .��5      �  � Color$ #BF953F��6#    � Offset$ 1Q�63   � 5      $ #AA771C��6#   $ 0Q�63   5      5      �� 5   
   .+�5      � $��100px��6   $��Center=�6"   .+�5      �� $/��)Обновление информации��6   $��36(�5      $
��Bold"�5      $��Center=�5      $��Center=�5      $��20 0 0 0q�5      5       � 5"      -   5        	 �Name$	 
redactGrid��5"      $y�	Collapsed=�6'   $��5q�6>   .+�5#      � 
 � Orientation$
 
Horizontal=�6   $��Center=�65   $��5 5 5 5q�6P   .+�5$      �� -   5         ��Name$ value1��5$      $��18(�6.   $��200px��6<   $��10q�6J   �� 5%      -   5        $ value2��5%      $��18(�6.   $��200px��6<   $��10q�6J   �� 5&      -   5        $ value3��5&      $��18(�6.   $��200px��6<   $��10q�6J   �� 5'      -   5        $ value4��5'      $��18(�6.   $��200px��6<   $��10q�6J   �� 5(      -   5        $ value5��5(      $��18(�6.   $��200px��6<   $��10q�6J   �� 5)      -   5         ��Name$ 	comboBox1��5)      $��18(�62   $��200px��6@   $��10q�6N   �� 5*      -   5        $ 	comboBox2��5*      $��18(�62   $��200px��6@   $��10q�6N   �� 5+      -	   5        $ 	comboBox3��5+      $��18(�62   $��200px��6@   $��10q�6N   �� 5,      -
   5        $ 	comboBox4��5,      $��18(�62   $��200px��6@   $��10q�6N   �� 5-      -   5         ��Name$ 
updateInfo��5-      $��100px��61   $��10q�6?   
��� ���
������$
��Ок��6~   6�   5/      50      &   System.Windows.Controls.DataGrid  52      -   5          Name$ dataGrid��53      $��
0 0 0 20pxq�54      $��890px��55         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 56         
IsReadOnly . 5:      5=      5>      5?      