   M S B A M L   `   `   `  ����   Heat Exchamger Lab*    $Heat_Exchamger_Lab.ExchangerDesigner   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  DESIGN NEW HEAT EXCHANGER�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ SingleBorderWindow=� � 
ResizeMode$ CanMinimize=� � ShowInTaskbar .  $	��545��$	��360�� � Icon$ Icons/ex.jpg��.���� .+���  �� Orientation$ 
Horizontal=�.+��� $	��200��
������ .+��� $��25��$��3q�$��
Identifier���� $��25��$��3q�$)��#Sell Internal Diameter         (Ds)���� $��25��$��3q�$&�� Tube External Diameter      (Do)���� $��25��$��3q�$'��!Tube Internal Diameter       (Di)���� $��25��$��3q�$)��#Number of Tubes                (nt)���� $��25��$��3q�$,��&Tube Length                        (L)���� $��25��$��3q�$/��)Tube Pitch                           (PT)���� $��25��$��3q�$%��Number of Tube Passes      (np)���� $��25��$��3q�$(��"Central Baffle Spacing         (B)���� $��25��$��3q�$,��&Inlet Baffle Spacing             (Bin)���� $��25��$��3q�$+��%Outlet Baffle Spacing          (Bout)���� $��25��$��3q�$+��%Fractional Baffle Cut            (Bc)���� $��25��$��3q�$*��$Number of Baffles               (nb)���� $��25��$��3q�$(��"Number of Sealing Strips    (Nss) ���� $��25��$��3q�$,��&Ratio of Sealing Strips         (rss) ���� $��25��$��3q�$��Tube Layout���� $��25��$��3q�$��	TEMA Type���� $��25��$��3q�$��Material of Construction���� $��25��$��3q�$��Include Safety Factor���� $	��100��
������ .+��� -    ��Name$ txtIdentifier��$��25��$	��0,2q� �� TextWrapping$ NoWrap=�
���   ��� -   	 ��Name$	 	stkValues��.+��� -   $ txtDs��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $ txtDo��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $ txtDi��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $ txtNt��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $
 txtL��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $ txtPt��$��25��$	��0,2q�$ NoWrap=�
���   ��� -	   $ txtnp��$��25��$	��0,2q�$ NoWrap=�
���   ��� -
   $
 txtB��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $ txtBin��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $ txtBout��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $ txtBc��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $ txtnb��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $ txtNss��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   $ txtrss��$��25��$	��0,2q�$ NoWrap=�
���   ��� -   
 ��Name$
 cmbTubeLayout�� �� SelectedIndex$ 0��$��25��$	��0,2q�
���   �.@��� $��
Triangular���� $��Square���� $��Rotated Square���� -   $
 cmbTemaType��$ 0��$��25��$	��0,2q�
���   �.@��� $	��AEM���� $	��BEM���� $	��NEN���� $	��AEU���� $	��BEU���� $	��AES���� $	��BES���� $	��AEW���� $	��BEW���� $	��AEP���� $	��BEP���� $	��AET���� $	��BET���� -   $
 cmbMaterial��$ 0��$��25��$	��0,2q�
���   �.@��� $��Carbon-Steel���� $��Admiralty-Brass���� $��	Aluminium���� $��Copper���� -    ��Name$ chkIncludeSafety��$	��0,5q� d� 	IsChecked$
 TrueY��� $��45��
������ .+��� -   $
 cmbUnitofDs��$��25��$��3q�$ 0��$��0,25,0,0q�.@��� $��25��$��m���� $��25��$��ft���� $��25��$��mm���� $��25��$��in���� -   $
 cmbUnitofDo��$��25��$ 0��$��3q�.@��� $��25��$��m���� $��25��$��ft���� $��25��$��mm���� $��25��$��in���� -   $
 cmbUnitofDi��$��25��$ 0��$��3q�.@��� $��25��$��m���� $��25��$��ft���� $��25��$��mm���� $��25��$��in���� -   $
 
cmbUnitofL��$��0,25,0,0q�$��25��$ 0��$��3q�.@��� $��25��$��m���� $��25��$��ft���� $��25��$��mm���� $��25��$��in���� -   $
 cmbUnitofPT��$��25��$��3q�$ 0��.@��� $��25��$��m���� $��25��$��ft���� $��25��$��mm���� $��25��$��in���� -   $
 
cmbUnitofB��$��25��$��0,25,0,0q�$ 0��$��3q�.@��� $��25��$��m���� $��25��$��ft���� $��25��$��mm���� $��25��$��in���� -   $
 cmbUnitofBin��$��25��$��3q�$ 0��.@��� $��25��$��m���� $��25��$��ft���� $��25��$��mm���� $��25��$��in���� -   $
 cmbUnitofBout��$��25��$ 0��$��3q�.@��� $��25��$��m���� $��25��$��ft���� $��25��$��mm���� $��25��$��in���� $��1��
���   �$��2��$	��345���� $ 
Horizontal=�$��25��$��0,3,0,0q�
������ .+��� -    ��Name$ btnView��$��25��$��70��$��	170,0,0,0q�$
��View���� -   $ btnSave��$��70��$��25��$��20,0,0,0q�$
��Save��