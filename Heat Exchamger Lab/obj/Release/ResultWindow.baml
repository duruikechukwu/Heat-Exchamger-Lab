   M S B A M L   `   `   `  ����   Heat Exchamger Lab%    Heat_Exchamger_Lab.ResultWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  RESULT SHEET��$	��768��$
��1366�� � WindowState$ 	Maximized=�$	��768��$
��1366�� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .   � Icon$ Icons/Table.ico��.��� .+�&   System.Windows.Controls.DataGrid  -     Name$ dataGridResult��   
IsReadOnly . 
����ح�   Columns 0  *System.Windows.Controls.DataGridTextColumn  -   ColumnRunNo��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.5* 	   Header$		 Run��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *runNo+   ClipboardContentBinding Z�   -   ColumnTF��$ * $	 
Tube Fluid��
 �� *TF+ Z�   -   ColumnSF��$ * $	 Shell Fluid��
 �� *SF+ Z�   -   ColumnHF��$ * $	 	Hot Fluid��
 �� *HF+ Z�   -   ColumnExchanger��$ * $	 	Exchanger��
 �� *	EXchanger+ Z�   -   	ColumnSVF��$ * $	 fo��
 �� *SVF+ Z�   -   	ColumnTVF��$ * $	 fi��
 �� *TVF+ Z�   -	   	ColumnSMF��$ * $	 mo��
 �� *SMF+ Z�   -
   	ColumnTMF��$ * $	 mi��
 �� *TMF+ Z�   -   	ColumnThi��$ * $		 Thi��
 �� *Thi+ Z�   -   	ColumnTho��$ * $		 Tho��
 �� *Tho+ Z�   -   	ColumnTci��$ * $		 Tci��
 �� *Tci+ Z�   -   	ColumnTco��$ * $		 Tco��
 �� *Tco+ Z�   -   Columnhi��$ * $	 hi��
 �� *hi+ Z�   -   Columnho��$ * $	 ho��
 �� *ho+ Z�   -   ColumnU��$ * $	 U��
 �� *U+ Z�   -   ColumnQ��$ * $	 Q��
 �� *Q+ Z�   -   ColumnA��$ * $	 A��
 �� *A+ Z�   -   	ColumnREo��$ * $		 REo��
 �� *REo+ Z�   -   	ColumnREi��$ * $		 REi��
 �� *REi+ Z�   -   	ColumnDPo��$ * $		 DPo��
 �� *DPo+ Z�   -   	ColumnDPi��$ * $		 DPi��
 �� *DPi+ Z� 