   M S B A M L   `   `   `  ����   Heat Exchamger Lab'    !Heat_Exchamger_Lab.CaseStudyWinow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  
CASE STUDY�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ SingleBorderWindow=� � 
ResizeMode$ CanMinimize=� � ShowInTaskbar .  $	��355��$	��550�� � Icon$ Icons/plot.jpg��.���� .+�&   System.Windows.Controls.DataGrid  -     Name$ datalog��   
IsReadOnly . $��80��   Columns 0  *System.Windows.Controls.DataGridTextColumn  -   ColumnIndependent��,  &System.Windows.Controls.DataGridColumn	   Width5  /System.Windows.Controls.DataGridLengthConverter$
	 0.5* 
   Header$
 Independent Variable (X)��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *X+   ClipboardContentBinding Z�   -   ColumnDependent��$
	 0.5* $
 Dependent Variable (Y)�� �� *Y+ Z� �� $��25�� �� Orientation$ 
Horizontal=�.+���  �� Padding$ 3q�$��RUN TO STUDY��$	��200���� -    ��Name$ cmbrun��$	��330���� $ 
Horizontal=�$��25��.+��� $ 3q�$ ��INDEPENDENT VARIABLE   (X)��$	��200���� -    ��Name$ txtindependent��$ 3q�$	��210���� -   $ txtindepunit��$ 3q�$	��120���� $ 
Horizontal=�$��25��.+��� $ 3q�$"��DEPENDENT VARIABLE       (Y)��$	��200���� -   $ txtdependent��$ 3q�$	��210���� -   $ 
txtdepunit��$ 3q�$	��120���� $ 
Horizontal=�$��25��.+��� $ 3q�$��START POINT��$	��200���� -	    ��Name$ txtstart��$��3q�$	��330���� $ 
Horizontal=�$��25��.+��� $ 3q�$��	END POINT��$	��200���� -
   $ txtend��$��3q�$	��330���� $ 
Horizontal=�$��25��.+��� $ 3q�$
��STEP��$	��200���� -   $ txtstep��$��3q�$	��330���� -    ��Name$ btnGo��$��0,10,0,0q�$��GENERATE POINTS���� -   $ btnView��$	��0,2q�$��	SHOW PLOT���� -   $ 	txtstatus��$��20��
���   �$��Ready�� �� TextAlignment$ Center=�
�������$
��Bold"�