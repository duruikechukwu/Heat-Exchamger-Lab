   M S B A M L   `   `   `  ����  Heat Exchamger Lab'    !Heat_Exchamger_Lab.CaseStudyWinow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
CASE STUDY��5   
    � WindowStartupLocation$ CenterScreen=�6    � WindowStyle$ SingleBorderWindow=�6B    � 
ResizeMode$ CanMinimize=�6c    � ShowInTaskbar .  $	��355��6�   $	��550��6�    � Icon$ Icons/plot.jpg��6�   .��5      �� .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ datalog��5         
IsReadOnly . $��80��66      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        ColumnIndependent��,  &System.Windows.Controls.DataGridColumn	   Width5  /System.Windows.Controls.DataGridLengthConverter$
	 0.5* 5   y   
   Header$
 Independent Variable (X)��6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *X+   ClipboardContentBinding 6V   Z� 6�     5	      -   5        ColumnDependent��$
	 0.5* 5	   w   $
 Dependent Variable (Y)��6�    6>   �� *Y+ 6T   Z� 6�   5      �� 5   
   $��25��6    �� Orientation$ 
Horizontal=�6!   .+�5      ��  �� Padding$ 3q�6   $��RUN TO STUDY��6$   $	��200��68   �� 5      -   5         ��Name$ cmbrun��5      $	��330��6'   5      �� 5   
   $ 
Horizontal=�6   $��25��6.   .+�5      �� $ 3q�6   $ ��INDEPENDENT VARIABLE   (X)��6$   $	��200��6F   �� 5      -   5         ��Name$ txtindependent��5      $ 3q�60   $	��210��6<   �� 5      -   5        $ txtindepunit��5      $ 3q�6.   $	��120��6:   5      �� 5   
   $ 
Horizontal=�6   $��25��6.   .+�5      �� $ 3q�6   $"��DEPENDENT VARIABLE       (Y)��6$   $	��200��6H   �� 5      -   5        $ txtdependent��5      $ 3q�6.   $	��210��6:   �� 5      -   5        $ 
txtdepunit��5      $ 3q�6,   $	��120��69   5      �� 5   
   $ 
Horizontal=�6   $��25��6.   .+�5      �� $ 3q�6   $��START POINT��6$   $	��200��67   �� 5      -	   5         ��Name$ txtstart��5      $��3q�6(   $	��330��64   6@   5      �� 5   
   $ 
Horizontal=�6   $��25��6.   .+�5       �� $ 3q�6   $��	END POINT��6$   $	��200��65   �� 5!      -
   5        $ txtend��5!      $��3q�6&   $	��330��62   6>   5#      �� 5$   
   $ 
Horizontal=�6   $��25��6.   .+�5%      �� $ 3q�6   $
��STEP��6$   $	��200��60   �� 5&      -   5        $ txtstep��5&      $��3q�6'   $	��330��63   6?   5(      �� 5)   
   -   5         ��Name$ btnGo��5)      $��0,10,0,0q�6    $��GENERATE POINTS��62   6L   �� 5*   
   -   5        $ btnView��5*      $	��0,2q�6"   $��	SHOW PLOT��6/   6C   �� 5+   
   -   5        $ 	txtstatus��5+      $��20��6'   
���   �$��Ready��6F    �� TextAlignment$ Center=�6S   
�������$
��Bold"�6�   5,      5-      