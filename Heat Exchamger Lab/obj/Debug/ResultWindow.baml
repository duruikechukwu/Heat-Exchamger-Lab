   M S B A M L   `   `   `  ����  Heat Exchamger Lab%    Heat_Exchamger_Lab.ResultWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  RESULT SHEET��5   	   $	��768��6   $
��1366��6+    � WindowState$ 	Maximized=�68   $	��768��6Q   $
��1366��6a    � WindowStartupLocation$ CenterScreen=�6r    � ShowInTaskbar .   � Icon$ Icons/Table.ico��6�   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridResult��5         
IsReadOnly . 
����ح�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        ColumnRunNo��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.5* 5   w   	   Header$		 Run��6�   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6:   �� *runNo+   ClipboardContentBinding 6T   Z� 6�     5
      -   5        ColumnTF��$ * 5
   q   $	 
Tube Fluid��6{   
 67   �� *TF+ 6N   Z� 6{     5      -   5        ColumnSF��$ * 5   q   $	 Shell Fluid��6|   
 67   �� *SF+ 6N   Z� 6|     5      -   5        ColumnHF��$ * 5   q   $	 	Hot Fluid��6|   
 67   �� *HF+ 6N   Z� 6|     5      -   5        ColumnExchanger��$ * 5      $	 	Exchanger��6�   
 6>   �� *	EXchanger+ 6\   Z� 6�     5      -   5        	ColumnSVF��$ * 5   s   $	 fo��6~   
 68   �� *SVF+ 6P   Z� 6~     5      -   5        	ColumnTVF��$ * 5   s   $	 fi��6~   
 68   �� *TVF+ 6P   Z� 6~     5      -	   5        	ColumnSMF��$ * 5   s   $	 mo��6~   
 68   �� *SMF+ 6P   Z� 6~     5      -
   5        	ColumnTMF��$ * 5   s   $	 mi��6~   
 68   �� *TMF+ 6P   Z� 6~     5      -   5        	ColumnThi��$ * 5   s   $		 Thi��6}   
 68   �� *Thi+ 6P   Z� 6}     5      -   5        	ColumnTho��$ * 5   s   $		 Tho��6~   
 68   �� *Tho+ 6P   Z� 6~     5      -   5        	ColumnTci��$ * 5   s   $		 Tci��6~   
 68   �� *Tci+ 6P   Z� 6~     5      -   5        	ColumnTco��$ * 5   s   $		 Tco��6~   
 68   �� *Tco+ 6P   Z� 6~     5      -   5        Columnhi��$ * 5   q   $	 hi��6|   
 67   �� *hi+ 6N   Z� 6|     5      -   5        Columnho��$ * 5   q   $	 ho��6|   
 67   �� *ho+ 6N   Z� 6|     5      -   5        ColumnU��$ * 5   o   $	 U��6z   
 66   �� *U+ 6L   Z� 6z     5      -   5        ColumnQ��$ * 5   o   $	 Q��6z   
 66   �� *Q+ 6L   Z� 6z     5      -   5        ColumnA��$ * 5   o   $	 A��6z   
 66   �� *A+ 6L   Z� 6z     5      -   5        	ColumnREo��$ * 5   s   $		 REo��6~   
 68   �� *REo+ 6P   Z� 6~     5      -   5        	ColumnREi��$ * 5   s   $		 REi��6~   
 68   �� *REi+ 6P   Z� 6~     5      -   5        	ColumnDPo��$ * 5   s   $		 DPo��6~   
 68   �� *DPo+ 6P   Z� 6~     5      -   5        	ColumnDPi��$ * 5   s   $		 DPi��6~   
 68   �� *DPi+ 6P   Z� 6~   5#      5$      5%      