   M S B A M L   `   `   `  ����  Heat Exchamger Lab$    Heat_Exchamger_Lab.FluidViewer   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$   FLUID TRANSPORT PROPERTIES��5   	   $	��550��6,   $	��790��69    � ShowInTaskbar .   � 
ResizeMode$ CanMinimize=�6[    � WindowStartupLocation$ CenterScreen=�6t    � Icon$ Icons/fl.jpg��6�   .��5      �� .+�5   
   �� $��25��6    �� Orientation$ 
Horizontal=�6!   .+�5      �� $
��Name��6   $��3q�6#   $��25��6/   $��50��6<   
�������$��20,0,0,0q�6^   �� 5      -   5         ��Name$ txtFluidName��5      $	��200��6,   $��25��68    �� TextWrapping$ NoWrap=�6D    �� TextAlignment$
 Left=�6Z   	 �� 
IsReadOnly	 . 6o   5	      �� 5
   
   $��25��6   $ 
Horizontal=�6!   $
��0,10q�6:   .+�5      �� $��Boiling Point��6   $��3q�6,   $	��100��68   
�������$��20,0,0,0q�6[   �� 5      -   5        $ txtBp��5      $	��150��6%   $��25��61   $ NoWrap=�6=   $
 Left=�6S   	 . 6h   �� 5      -   5        
 ��Name$
 	lblBpunit��5      $��3q�6'   $��50��63   �� 5      $��Melting Point��6   $��3q�6,   $	��100��6:   
�������$��20,0,0,0q�6]   �� 5      -   5        $ txtMp��5      $	��150��6%   $��25��61   $ NoWrap=�6=   $
 Left=�6S   	 . 6h   �� 5      -   5        $
 	lblMpunit��5      $��3q�6'   $��50��63   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datagrid��5      $	��400��6%   $	��750��62   $��17,10q�6>      
IsReadOnly . $��Bottom=�6_      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        colT��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5   o      Header$ Temperature (��6y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Temp+   ClipboardContentBinding 6L   Z� 6y     5      -   5        	colCp��$ * 5   n   $
 Cp (��6x    64   �� *Cp+ 6K   Z� 6x     5      -	   5        
colMiu��$ * 5   p   $ Viscosity (��6z    65   �� *MIU+ 6M   Z� 6z     5      -
   5        
colRho��$ * 5   p   $ 	Density (��6z    65   �� *RHO+ 6M   Z� 6z     5      -   5        colK��$ * 5   l   $	 K (��6v    63   �� *K+ 6I   Z� 6v   5      5"      5#      