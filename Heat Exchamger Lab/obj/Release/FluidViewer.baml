   M S B A M L   `   `   `  ����   Heat Exchamger Lab$    Heat_Exchamger_Lab.FluidViewer   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$   FLUID TRANSPORT PROPERTIES��$	��550��$	��790�� � ShowInTaskbar .   � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � Icon$ Icons/fl.jpg��.���� .+��� $��25�� �� Orientation$ 
Horizontal=�.+��� $
��Name��$��3q�$��25��$��50��
�������$��20,0,0,0q��� -    ��Name$ txtFluidName��$	��200��$��25�� �� TextWrapping$ NoWrap=� �� TextAlignment$
 Left=�	 �� 
IsReadOnly	 . �� $��25��$ 
Horizontal=�$
��0,10q�.+��� $��Boiling Point��$��3q�$	��100��
�������$��20,0,0,0q��� -   $ txtBp��$	��150��$��25��$ NoWrap=�$
 Left=�	 . �� -   
 ��Name$
 	lblBpunit��$��3q�$��50���� $��Melting Point��$��3q�$	��100��
�������$��20,0,0,0q��� -   $ txtMp��$	��150��$��25��$ NoWrap=�$
 Left=�	 . �� -   $
 	lblMpunit��$��3q�$��50��&   System.Windows.Controls.DataGrid  -     Name$ datagrid��$	��400��$	��750��$��17,10q�   
IsReadOnly . $��Bottom=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  -   colT��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    Header$ Temperature (��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Temp+   ClipboardContentBinding Z�   -   	colCp��$ * $
 Cp (�� �� *Cp+ Z�   -	   
colMiu��$ * $ Viscosity (�� �� *MIU+ Z�   -
   
colRho��$ * $ 	Density (�� �� *RHO+ Z�   -   colK��$ * $	 K (�� �� *K+ Z� 