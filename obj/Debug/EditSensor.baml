   M S B A M L   `   `   `  ����K  GSmartHouseClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SmartHouseClientSmartHouseClient  5   	   !    SmartHouseClient.EditSensor   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:SmartHouseClient     5   	     � Title$<  6Изменение параметров датчика��5   	   $��203.192��6.   $��303.191��6?   .��5	      � .+�5
   
   �� $��14(�6   $	��Top=�6   $
��Left=�67   $��	15,20,0,0q�6R   .��6e   0.Порог нештатной ситуации6   �� 5   
   -   5         ��Name$ checkTxt��5      $��30��6$   $��50��60   $
��Left=�6<   $	��Top=�6W   $��
210,20,0,0q�6o   6�   �� 5   
   -   5        $��30��5      $	��170��6   $��Bottom=�6)   $��10q�6D   .��6e   42Изменить параметры датчика6�   � 5   
   -   5         �Name$ AnalogContorls��5      .+�5   
   �� $��14(�6   $	��Top=�6   $
��Left=�67   $��	10,90,0,0q�6R   $��Верхний порог��6e   �� 5   
   -   5        $ upTxt��5      $��30��6!   $��50��6-   $
��Left=�69   $	��Top=�6T   $��
120,90,0,0q�6l   �� 5   
   -   5        $ downTxt��5      $��30��6#   $��50��6/   $
��Left=�6;   $	��Top=�6V   $��
120,50,0,0q�6n   �� 5   
   $��14(�6   $	��Top=�6   $
��Left=�67   $��	10,50,0,0q�6R   .��6e   Нижний порог6s   5      5      5      