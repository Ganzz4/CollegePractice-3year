   M S B A M L   `   `   `  ����@  <Lab11, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Lab11.FormsLab11.Forms  5   	        Lab11.Forms.AdminPanelForm   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   %localclr-namespace:Lab11.Forms  5   	     � Title$/  )Панель администратора��5	   	   $	��450��6'    � Icon$! ../Icons/AdminPanelIcon.png��64   $	��800��6W    � WindowStartupLocation$ CenterScreen=�6c    � 
ResizeMode$ NoResize=�6�   .��5      � ��5   
   �� 5       �� 
StartPoint$ 0.1,0&�6"    �� EndPoint$ 0.9,1&�65   .��5      �  � Color$ 	#FFE63070��6    � Offset$ 1Q�61   � 5      $ 	#FFFE8704��6   $ 0Q�61   5      .+�5   
   �� $	��390��6   $	��Top=�6    �� CornerRadius$ 	0 0 180 0��66   ���#100E176O   �� 5   
   	 �� Orientation$	 
Horizontal=�6   .+�5      �� $	��300��6   $��Center=�6%   .+�5      �� $!��../Icons/AdminPanelIcon.png��6   $	��250��6=   $	��250��6I   $	��Top=�6V   $��0q�6n   $
��Fill=�6y   5      �� 5      $	��400��6   .+�5      �� -   5        
 ��Name$
 	MainPanel��5   ,   $��20 40q�6   .+�5      �� -   5         ��Name$ TopPanelText��5   9   $'��!Панель управления��6    $��20q�6O   
������� �� TextAlignment$ Center=�6n   $��34(�6�   $��Great Vibes)�6�   6�   �� 5      -   5        $
 buttonsEntryReg��5   J   $��10q�6!   $��Center=�6-   .+�5       �� -   5         ��Name$ changeRoleUser��5"   E   $	��300��5    !   $��40��6-      MaterialDesignRaisedButton#��[  69   $��0 5 0 0q�6m   +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��5!      $��2q�6H   ���#D94448���#D94448
�������.��5$      �� $	 
Horizontal=�6)   $��Center=�6B   .+�5%   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 User=�6:   
�������$��30��6Y   $��30��6d   $��Center=�6p   �� 5&   "   $7��1Управление пользователями��6,   $��10 0 0 0q�6M   $��Center=�6_   5'      5(      �� 5+      -   5        $ createBackup��5-   E   $	��300��5+   !   $��40��6-   #��[  69   $��0 20 0 0q�6m   $ 10��5,      $��2q�6H   ���#D94448���#D94448
�������.��5/      �� $	 
Horizontal=�6)   $��Center=�6B   .+�50   "     $ DatabaseAdd=�6:   
�������$��30��6`   $��30��6k   $��Center=�6w   �� 51   "   $2��,Создать резервную копию��6,   $��10 0 0 0q�6K   $��Center=�6]   52      53      �� 55      -   5        $ restoreBackup��57   E   $	��300��55   !   $��40��6-   #��[  69   $��0 20 0 0q�6m   $ 10��56      $��2q�6H   ���#D94448���#D94448
�������.��59      �� $	 
Horizontal=�6)   $��Center=�6B   .+�5:   "     $ DatabaseSync=�6:   
�������$��30��6a   $��30��6l   $��Center=�6x   �� 5;   "   $<��6Восстановить резервную копию��6,   $��10 0 0 0q�6P   $��Center=�6b   5<      5=      5@      5A      5B      �� 5C      $	��100��6   .+�5D      �� -   5        $ 
buttonExit��5E   "   $��10 20q�5D       & "MaterialDesignFloatingActionButton#��[ 6(    � ToolTip$ Close��5E      ��5D   d   Z� .��5F        $ Close=�6.   
�������$��30��6N   $��30��6Y   5G      5H      5I      5J      5K      