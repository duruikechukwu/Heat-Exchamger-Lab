   M S B A M L   `   `   `  ����   Heat Exchamger Lab    Heat_Exchamger_Lab.Store   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  LABORATORY STORE��$	��450��$	��340�� � 
ResizeMode$ CanMinimize=� � ShowInTaskbar .   � Icon$ Icons/Store.ico��.���� $��Center=�.+���  �� Orientation$ 
Horizontal=�$	��200��$	��Top=�.+��� $��2,0,0,0q�$��HEAT EXCHANGERS��$	��150��
���   �$��1q�.���� -    ��Name$ lstExchanger���� $��2,0,0,0q�$��HEAT TRANSFER FLUIDS ��$	��150��
���   �$��1q�.���� -   $ lstFluid��$��2,0,-2,0q��� $��2,0,0,0q�$ 
Horizontal=�$��25��.+��� -    ��Name$ btnAddExchanger��$��Equip Exchanger��$	��100���� -   $ btnAddToHot��$��Add To Hot Tank��$	��100���� -   $ btnAddToCold��$ ��Add To Cold Tank Exchanger��$	��100���� 
������ $	��119��.+��� $��25��$ 
Horizontal=�$��0,5,0,0q�.+��� $��Exchanger In Use��$��3q�$	��100��
�������$��2,0,0,0q��� -    ��Name$ txtEX��$	��200��$��25�� �� TextWrapping$ NoWrap=�	 �� TextAlignment$
	 Left=�
 �� 
IsReadOnly
 . �� $��25��$ 
Horizontal=�$��0,5,0,0q�.+��� $��Hot Fluid In Use��$��3q�$	��100��
�������$��2,0,0,0q��� -   $ txtHF��$	��200��$��25��$ NoWrap=�$
	 Left=�
 . �� $��25��$ 
Horizontal=�$��0,5,0,0q�.+��� $��Cold Fluid In Use��$��3q�$	��100��
�������$��2,0,0,0q��� -   $ txtCF��$	��200��$��25��$ NoWrap=�$
	 Left=�
 . �� $��2,0,0,0q�$ 
Horizontal=�$��25��.+��� -	   $ btnViewExchanger��$��View Heat Exchanger��$	��150���� -
   $ btnViewFluid��$��View Fluid Properties��$	��150���� $��2,20,0,0q�$ 
Horizontal=�$��25��.+��� -   $ btnDeleteExchanger��$��Delete Heat Exchanger��$��18��$
��0,-2q�$	��150���� -   $ btnDeleteFluid��$��Delete Fluid ��$��18��$
��0,-2q�$	��150��