   M S B A M L   `   `   `  ����@  <Lab11, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Lab11.FormsLab11.Forms  5   	       Lab11.Forms.UserManagment   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   %localclr-namespace:Lab11.Forms  5   	     � Title$7  1Управление пользователями��5	   	   $	��800��6+   $
��1000��68    � WindowStartupLocation$ CenterScreen=�6E    � Icon$! ../Icons/AdminPanelIcon.png��6j   .��5
      � ���#e0e1dd.+�5   
   �� $	��100��6   $	��Top=�6    �� CornerRadius$ 	0 0 120 0��66   ��5      �� 5       �� 
StartPoint$ 0.1,0&�6&    �� EndPoint$ 0.9,1&�69   .��5      �  � Color$ #BF953F��6#    � Offset$ 1Q�63   � 5      $ #AA771C��6#   $ 0Q�63   5      5      �� 5   
   .+�5      � $��100px��6   $��Center=�6"   $��Center=�6=   $��0 0 0 20q�6Z   .+�5      �� $7��1Управление пользователями��6   $��44(�6=   $
��Bold"�6K   $��Center=�6]   $��20 0 0 0q�6x   5      o� 5      -   5         o�Name$ mainMenu��5      $
��24px(�6%   $��Center=�65   $��0 0 0 20q�6R   	 � 	Resources	 5      
 �� 
TargetType   MenuItemStyle �� Property �� Value � ColumnDefinitions'  !MaterialDesignThemes.Wpf.PackIcon   Kind �� RelativeSource �� AncestorType �� Path��%�  &	        �� 5      "
 m�6   .�5      ��  � ��$ 	20 0 20 0q�64   �� 5       � �� 5      �� 5   "   .R�5   &   � $
��Auto��6+   $
��Auto��68    5    *   �� 5!   .   $
��Auto �6?   �� 5"   .   $��* �6?   .+�5$   *     $��40��6B   $��40��6M   $��5q�6�    6Y   ��  �� $ MenuItemL�$	 Tag�6�   �� 5%   *   $��1��6;   #��z��6K   $��Center=�6o   5&   '   5'   #   5)      5*      5+      .@�5,      m� -   5         m�Name$ addInfo��5,   -   $��Добавить��6   #��[  6>   
 � Tag$ 	AddCircle��6e   6u   m� 5-      -   5        $ 
deleteInfo��5-   -   $��Удалить��6   #��[  6A   $ Delete��6h   6u   m� 5.      -   5        $ redact��5.   3   $ ��Редактировать��6   #��[  6C   $
 Edit��6j   6u   m� 5/      -   5        $ findMenuItem��5/      $��	20 0 20 0q�61   $	��250��6E   ��50      �� 51       �� 
TargetType" m�6*   .R�52      �  53   "   �� 54   &   $	��200 �67   �� 55   &   $��50 �67   .+�57   "   ��  ��Name$ textBoxFind��6*   $��0��6?   $��0q�6O   $��5q�6c   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$" Поле для поиска��6n     58   "   -   5        $ 
FindInPage=�58   :   $��40��6L   $��40��6W   $��1��6d    � Cursor$
 Hand��6t   6�   59      5:      5<      5=      &   System.Windows.Controls.DataGrid  5@      -   5          Name$ dataGrid��5A      $��
0 0 0 20pxq�5B      $��500px��5C         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5D         
IsReadOnly .   MaterialDesignDataGrid#��[ 5G      5I      5J      5K      