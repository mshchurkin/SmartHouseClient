   M S B A M L   `   `   `  ����K  GSmartHouseClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SmartHouseClientSmartHouseClient  5   	        SmartHouseClient.AddSensor   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:SmartHouseClient     5   	     � Title$)  #Добавление датчика��5   	   $��348.68��6$   $	��345��64   .��5	      � .+�5
   
   �� $��14(�6   $	��Top=�6   $
��Left=�67   $��	15,10,0,0q�6R   .��6e   Наименование6s   �� 5   
   -   5         ��Name$ nameBox��5      $��30��6#   $	��200��6/   $
��Left=�6<   $	��Top=�6W   $��	15,40,0,0q�6o   6�   � 5   
   -   5         �Name$ AnalogControls��5      .+�5      �� $��14(�6   $	��Top=�6"   $
��Left=�6;   $��
10,100,0,0q�6V   .��6j   0.Порог нештатной ситуации6�   �� 5      -   5        $ checkTxt��5      $��30��6(   $��50��64   $
��Left=�6@   $	��Top=�6[   $��
15,130,0,0q�6s   6�   �� 5      $��14(�6   $	��Top=�6"   $
��Left=�6;   $��
10,200,0,0q�6V   $��Верхний порог��6j   �� 5      -   5        $ upTxt��5      $��30��6%   $��50��61   $
��Left=�6=   $	��Top=�6X   $��120,200,0,0q�6p   �� 5      -   5        $ downTxt��5      $��30��6'   $��50��63   $
��Left=�6?   $	��Top=�6Z   $��120,160,0,0q�6r   �� 5      $��14(�6   $	��Top=�6"   $
��Left=�6;   $��
10,160,0,0q�6V   .��6j   Нижний порог6x   5      	� 5   
   -   5         	�Name$ discrete��5      $��40��6(   $��	20,0,0,40q�64   $��Bottom=�6G   .��6}   Дискретный6�   	� 5   
   -   5        $ analog��5      $��40��6&   $��
150,0,0,40q�62   $��Аналоговый��6F   $��Bottom=�6[   6v   �� 5   
   -   5        $��30��5      $	��150��6   $��Bottom=�6)   $��10q�6D   .��6e   Добавить датчик6v   5      5      