   M S B A M L   `   `   `  ����   FindInDrive    FindInDrive.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  FindInDrive V1.1��$	��800��$	��700�� � 
ResizeMode$ NoResize=�.���  � RowDefinitions �� $��90 ��� $��75 ��� $��50 ��� $��60 ��� $	��200 ��� $	��200 ��� $��70 ���  � ColumnDefinitions �� $	��275 ��� $
��250* �.+��� -    ��Name$ lblDriveName��$��Enter Drive Name  :��$��Center=�$	��Top=�$��0��$��0��$��
78,10,83,0q�$	��114���� $��Example : C:\Utilities��$��0��$��0��$
��Left=�$��	78,40,0,0q�$	��Top=�$	��162�����	#FF2AB268�� -    ��Name$ txtDriveName��$�� ��$��Center=�$	��Top=�$��1��$	��272��$��30��$��17,10,39,10q��� -   $��chkAllDrives��$��Search All The Drives��$��1��$��31��$
��Left=�$��	63,59,0,0q�$	��Top=�$	��214���� -   $ lblSearchText��$��Enter Search Text  :��$��Center=�$	��Top=�$��0��$��1��$��26��$��
78,11,93,0q�$	��104���� $ ��Example : ConnectionString��$
��Left=�$��	78,37,0,0q�$��0��$��1��$	��Top=�$	��162�����	#FF2AB268�� -   $ txtSearchText��$�� ��$��Center=�$	��Top=�$��1��$��1��$	��272��$��30��$��17,10,39,10q��� -   $��chkIsCaseSensitive��$��Case Sensitive��$��1��$
��Left=�$��	63,50,0,0q�$��1��$	��Top=��� -   $ lblOutputFileLocation��$)��#Enter Search Result File Location :��$��Center=�$	��Top=�$��0��$��2��$��26��$��
78,10,17,0q�$	��180���� $��Example : C:\MyFolder��$
��Left=�$��	78,27,0,0q�$��0��$��2��$	��Top=�$	��180�����	#FF2AB268�� -   $ txtOutputFileLocation��$�� ��$��Center=�$	��Top=�$��1��$��2��$	��272��$��30��$��17,10,39,10q��� -	    ��Name$ 	btnSearch��$��Search��$��Center=�$	��Top=�$��1��$��3��$	��100��$��30��$��122,10q��� -
   $��label1��$��	Summary :��$��4��$��28��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��150���� -   $ 	txtOutput��$��Center=�$	��Top=�$��4��$	��500��$	��100��$
��Auto=�$
��Auto=�$��2��$��10,50,29,30q�$��14(��� -   $��
lblSummary��$"��UnAuthorized Drives /Files :��$��5��$��28��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��150���� -   $ txtUnAuthorizedDrives��$��Center=�$	��Top=�$��5��$	��500��$	��100��$
��Auto=�$
��Auto=�$��2��$��10,50,29,30q�$��14(��� -   $ lblError��$�� ��$
��Left=�$��31,8,0,0q�$��6��$	��Top=�$	��526��$��49��$��2��
���  ��$��16(�$
��Bold"�