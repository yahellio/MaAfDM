   M S B A M L   `   `   `  ����   MainPart    MainPart.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Laba 9��$	��600��$
��1000��$	��600��$	��800�����#f0f4f8$��Segoe UI)� � WindowStartupLocation$ CenterScreen=� � Icon$ 	/icon.png�� � 	Resources 3  -System.Windows.Media.Effects.DropShadowEffect   
CardShadow   
BlurRadius   ShadowDepth   Opacity   Color  	CardStyle �� 
TargetType	 �� Property
 �� Value  
TitleStyle  
ValueStyle  ProgressStyle  ActionButtonStyle �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value �� ScaleX �� ScaleY �� CenterX �� CenterY��%N  &	        &	 +     7   &	 �     &	 g    &	 e    &	 �      $ 12Q�$ 4Q�$	 0.1Q�$
 #333���� " ��.��� 	 � ��

 ������� 	 �@��CornerRadius$
 8���� 	 � ��$
 1q��� 	 � ��
 �#e0e0e0�� 	 �@J�Effect8
   �� 	 �@��Padding$
 15q��� 	 � ��$
 10q��� " ��.��� 	 � ��$
 18(��� 	 � ��$
 SemiBold"��� 	 � ��
 �#2c3e50�� 	 � ��$
 0 0 0 10q��� " ��.��� 	 � ��$
 16(��� 	 � ��
 �#34495e�� 	 � ��

 ������� 	 � ��$
 1q��� 	 � ��
 �#bdc3c7�� 	 � ��$
 8q��� 	 �@��
IsReadOnly
 . �� 	 �@��TextAlignment$
 Center=��� 	 � ��$
 Center=��� " �.��� 	 � ��$
 20���� 	 � ��
 �#3498db�� 	 � ��
 �#ecf0f1�� 	 � ��$
 0q��� 	 � ��$
 0 10 0 5q��� " ��.��� 	 � ��$
 5q��� 	 � ��$

 12 6q��� 	 � ��$
 14(��� 	 � ��

 ������� 	 � ��
 �#7f8c8d�� 	 � ��$
 2q��� 	 � ��

 �   ��� 	 � ��
 �� " ��.R��� #��z��#��z��#��z��$ 6��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� 	 � ��
 �#95a5a6�� 	 � ��
 �#95a5a6�� 	 � z�
 �� $
 1.05Q�$
 1.05Q�$	 0.5Q�$	 0.5Q�P�  � {� .  . ��� 	 � ��
 �#dfe6e9�� 	 � ��
 �#dfe6e9�� 	 � ��
 �#b2bec3.���  � ColumnDefinitions �� $��* ��� $	��320 �.+��� $��0��#��[ ._��� -    ��Name$ CurrentImage��$��Uniform=�$��5q��� $��1�����#f8fafc���#e0e6eb$��1 0 0 0q�._�� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� #��[ $��0��$��	0,10,0,10q�._��� .+��� $��Network Status��#��[ �� -    ��Name$ ClassificationResultLabel��#��[ � -    �Name$ TrainingProgressBar��#��[ �� -    ��Name$ TrainingStatusLabel�����#7f8c8d �� TextAlignment$ Center=�$	��0 5q��� #��[ $��1��$
��0 15q�._��� .+��� $��Network Management��#��[ �� -    ��Name$ OpenNetworkButton��$��Load Network��#��[ �� -   $ SaveNetworkButton��$��Save Network��#��[ {�.  �� #��[ $��2��$��	0,10,0,10q�._��� .+��� $��Network Training��#��[ �� -   $ TeachingButton��$��Start Training��#��[ �� $��4��$��Bottom=�.+��� -   $ LoadImageButton��$��
Load Image��#��[ {�.  �� -	   $ ClassificationButton��$��Classify��#��[ {�.  