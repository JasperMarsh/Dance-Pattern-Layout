Public Class MainForm

    Dim startx As Integer 'Global Variables for mouse drag
    Dim starty As Integer
    Dim endy As Integer
    Dim endx As Integer
    Dim finalx As Integer
    Dim finaly As Integer
    Dim mdown As Boolean
    Dim valx As Boolean
    Dim valy As Boolean

    Dim CheckClick As Boolean

    Public TotalMarkers As Integer = 32
    Dim CurrentPool() As Integer = {1, 2, 3, 4, 5, 6, 7, 8}
    Dim CustomPool() As Integer = {}
    Public BorderWidth As Integer = (Me.Width - Me.ClientSize.Width) / 2
    Public TitlebarHeight As Integer = Me.Height - Me.ClientSize.Height - 2 * BorderWidth
    'Dim AllMarkers() As {Marker1, Marker2, Marker3, Marker4, Marker5, Marker6, Marker7, Marker8, Marker9, Marker10, Marker11, Marker12, Marker13, Marker14, Marker15, Marker16, Marker17, Marker18, Marker19, Marker20, Marker21, Marker22, Marker23, Marker24, Marker25, Marker26, Marker27, Marker28, Marker29, Marker30, Marker31, Marker32}
    Public CheckNameTag() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}
    Public CheckColour() As String = {"Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black"}

    Public ImageLeft As Integer = 0
    Public ImageTop As Integer = 100
    Public ImageWidth As Integer = Me.ClientSize.Width
    Public ImageHeight As Integer = Me.ClientSize.Height - 100

    'Drag objects with mouse
    Private Sub MouseDrag(sender As Object)
        If mdown = True Then 'Check if mouse=down
            CheckClick = False
            endx = (MousePosition.X - Me.Left)
            endy = (MousePosition.Y - Me.Top)
            If valy = False Then
                starty = endy - sender.top
                valy = True
            End If
            If valx = False Then
                startx = endx - sender.left
                valx = True
            End If
            sender.left = endx - startx
            sender.top = endy - starty
        End If
    End Sub
    Private Sub MouseDragClick()
        startx = MousePosition.X
        starty = MousePosition.Y
        mdown = True
        valx = False
        valy = False
    End Sub
    Private Sub MouseDragUnclick()
        mdown = False
        valx = False
        valy = False
        CheckClick = True
    End Sub

    'Number to Object Converversions
    Public Function ReturnMarker(Number As Integer)
        'Dim i As String = "Marker" & CStr(Number)
        'Return Me.Controls.Find(1, False)
        'Dim Obj As Object = AllMarkers(Number)
        'Return Obj 'AllMarkers(Number - 1)
        Dim i As String = "Marker" & CStr(Number)
        Dim Obj As PictureBox = Marker1
        For Each elem In Me.Controls
            If elem.Name = i Then
                Obj = elem
                Exit For
            End If
        Next
        Return Obj
    End Function
    Public Function ReturnPointer(Number As Integer)
        Dim i As String = "Point_" & CStr(Number)
        Dim Obj As PictureBox = Point_1
        For Each elem In Me.Controls
            If elem.Name = i Then
                Obj = elem
                Exit For
            End If
        Next
        Return Obj
    End Function
    Public Function ReturnNumber(Obj As Object)
        Dim Str As String = Obj.Name
        Dim StrLen As Integer = Strings.Len(Obj.Name)
        Dim RightStr As Integer = Strings.Right(Str, StrLen - 6)
        Return RightStr
    End Function

    'Trigonometry Calculations
    Function VectorToPointX(x As Integer, Angle As Integer, Distance As Integer)
        Return x + System.Math.Sin(Angle / 57.2958) * Distance 'Converts degrees to radians
    End Function
    Function VectorToPointY(y As Integer, Angle As Integer, Distance As Integer)
        Return y - System.Math.Cos(Angle / 57.2958) * Distance 'Converts degrees to radians
    End Function
    Function ReturnDistance(m1x As Integer, m1y As Integer, m2x As Integer, m2y As Integer)
        Return System.Math.Sqrt(((m2x - m1x) ^ 2) + ((m2y - m1y) ^ 2))
    End Function
    Function ReturnDirection(m1x As Integer, m1y As Integer, m2x As Integer, m2y As Integer)
        Return System.Math.Atan2(m2x - m1x, m1y - m2y) * 57.2958
    End Function

    'Get colours for drawing
    Function MarkerColor(sender As Object)
        Dim CurrentNumber As Integer = ReturnNumber(sender) - 1
        Dim Result As New Drawing.SolidBrush(Color.Black)
        Select Case CheckColour(CurrentNumber)
            Case "Red"
                Result = New Drawing.SolidBrush(Color.Red)
            Case "Orange"
                Result = New Drawing.SolidBrush(Color.DarkOrange)
            Case "Yellow"
                Result = New Drawing.SolidBrush(Color.Yellow)
            Case "Green"
                Result = New Drawing.SolidBrush(Color.Green)
            Case "Blue"
                Result = New Drawing.SolidBrush(Color.Blue)
            Case "Purple"
                Result = New Drawing.SolidBrush(Color.Purple)
            Case "Brown"
                Result = New Drawing.SolidBrush(Color.SaddleBrown)
            Case "Grey"
                Result = New Drawing.SolidBrush(Color.Gray)
        End Select
        Return Result
    End Function
    Function NumberColour(sender As Object)
        Dim CurrentNumber As Integer = ReturnNumber(sender) - 1
        Dim BlackBrush As New Drawing.SolidBrush(Color.Black)
        Dim RedBrush As New Drawing.SolidBrush(Color.Red)
        Dim Result = RedBrush
        Select Case CheckColour(CurrentNumber)
            'Case "Black"
            '    Result = RedBrush
            Case "Red"
                Result = BlackBrush
            Case "Orange"
                Result = BlackBrush
            Case "Yellow"
                Result = BlackBrush
            Case "Green"
                Result = BlackBrush
            Case "Blue"
                Result = RedBrush
            Case "Purple"
                Result = BlackBrush
            Case "Brown"
                Result = BlackBrush
            Case "Grey"
                Result = BlackBrush
        End Select
        Return Result
    End Function

    'Move to Point
    Private Sub PositionCorrect(sender As Object)
        If sender.Top < 0 + TopBanner.Height Then
            sender.Top = 0 + TopBanner.Height
        ElseIf sender.Top > Me.ClientSize.Height - sender.Height Then
            sender.Top = Me.ClientSize.Height - sender.Height
        End If
        If sender.Left < 0 Then
            sender.Left = 0
        ElseIf sender.Left > Me.ClientSize.Width - sender.Width Then
            sender.Left = Me.ClientSize.Width - sender.Width
        End If
    End Sub
    Public Sub MoveMarkerTo(a As Integer, x As Integer, y As Integer)
        Dim b As Object = ReturnMarker(a)
        b.Top = y - (b.Height / 2)
        b.Left = x - (b.Width / 2)
        PositionCorrect(b)
    End Sub
    Private Sub MoveFromVector(a As Integer, m1x As Integer, m1y As Integer, Direction As Integer, Distance As Integer)
        Dim NewX As Integer = VectorToPointX(m1x, Direction, Distance)
        Dim NewY As Integer = VectorToPointY(m1y, Direction, Distance)
        MoveMarkerTo(a, NewX, NewY)
    End Sub
    Private Sub MoveTo(a As Object, x As Integer, y As Integer)
        a.Top = y
        a.Left = x
    End Sub

    'Marker Placements
    Private Sub MakeLine(m1x As Integer, m1y As Integer, m2x As Integer, m2y As Integer, pool As Array, OneOnOne As Boolean, Offset As Integer)
        Dim Distancex As Integer = m2x - m1x
        Dim Distancey As Integer = m2y - m1y
        Dim SampleSize As Integer = pool.Length - 1
        If SampleSize < 1 Then
            SampleSize = 1
        End If
        Dim Incrementx As Integer = Distancex / SampleSize
        Dim Incrementy As Integer = Distancey / SampleSize
        Dim Count As Integer = 0
        For Each a As Integer In pool
            Count = Count + 1
            MoveMarkerTo(a, (m1x + (Incrementx * (Count - 1))), (m1y + (Incrementy * (Count - 1))))
        Next
        If OneOnOne Then
            Dim Direction As Double = ReturnDirection(m1x, m1y, m2x, m2y)
            Dim Inverse As Integer = 1
            Dim CurrentMarker As Object = Marker1
            For Each a In pool
                CurrentMarker = ReturnMarker(a)
                MoveFromVector(a, CurrentMarker.Location.X, CurrentMarker.Location.Y, Direction + 90 * Inverse, Offset)
                Inverse = Inverse * -1
            Next
        End If
    End Sub
    Private Sub MakeLineBetween2(m1x As Integer, m1y As Integer, m2x As Integer, m2y As Integer, section1 As Integer, section2 As Integer)
        Dim Distancex As Integer = m1x - m2x
        Dim Distancey As Integer = m1y - m2y
        Dim SampleSize As Integer = section2 - section1
        If SampleSize = 0 Then
            SampleSize = 1
        End If
        Dim Incrementx As Integer = Distancex / SampleSize
        Dim Incrementy As Integer = Distancey / SampleSize
        Dim Count As Integer = 1
        For a As Integer = section1 To section2
            MoveMarkerTo(a, (m1x - (Incrementx * (Count - 1))), (m1y - (Incrementy * (Count - 1))))
            Count = Count + 1
        Next
    End Sub
    Private Sub MakeRows(m1x As Integer, m1y As Integer, m2x As Integer, m2y As Integer, Rows As Integer, Length As Integer, pool As Array)
        Dim DistanceVert As Integer = m1y - m2y
        Dim IncrementVert As Integer = DistanceVert / (Rows - 1)
        'Dim RowPool(Rows, Length) As Integer
        'Dim makePoolCount As Integer = 1
        'For Each a As Integer In pool
        '    If a > Rows * Length Then
        '        Exit For
        '    End If
        '    Dim CurrentRow = System.Math.Floor(makePoolCount / Length)
        '    Dim CurrentPosition = makePoolCount Mod Length - 1
        '    If CurrentPosition < 0 Then
        '        CurrentPosition = Length
        '    End If
        '    RowPool(CurrentRow, CurrentPosition) = a
        'Next
        Dim Count As Integer = 1
        For a As Integer = 0 To Rows - 1
            Dim Height As Integer = m1y - IncrementVert * (Count - 1)
            Dim section1 As Integer = (Length) * (Count - 1) + 1
            Dim section2 As Integer = Length * Count
            Dim CurrentRowPool() As Integer = {}
            For b As Integer = section1 To section2
                ReDim Preserve CurrentRowPool(CurrentRowPool.Length)
                CurrentRowPool(CurrentRowPool.Length - 1) = pool(b - 1)
            Next
            'Dim CurrentRowPool() As Integer = {}
            'For Each c In b
            '    ReDim Preserve CurrentRowPool(CurrentRowPool.Length)
            '    CurrentRowPool(CurrentRowPool.Length - 1) = c
            'Next
            MakeLine(m1x, Height, m2x, Height, CurrentRowPool, False, 0)
            Count = Count + 1
        Next
    End Sub
    Private Sub MakeCircle(m1x As Integer, m1y As Integer, Radius As Integer, Direction As Double, pool As Array, Clockwise As Boolean, Arc As Integer)
        Dim Inverse As Integer = 1
        If Not Clockwise Then
            Inverse = -1
        End If
        Dim SampleSize As Integer = pool.Length
        Dim IncrementR As Double = Arc / SampleSize * Inverse
        Dim Count As Integer = 1
        For Each a As Integer In pool
            MoveFromVector(a, m1x, m1y, (Direction + (IncrementR * (Count - 1))), Radius)
            Count = Count + 1
        Next
    End Sub
    Private Sub MakeCurve(m1x As Integer, m1y As Integer, Distance As Double, Direction As Integer, Arc As Integer, pool As Array, Clockwise As Boolean)
        Dim Inverse As Integer = 1
        If Not Clockwise Then
            Inverse = -1
        End If
        Dim SampleSize As Integer = pool.Length
        Dim IncrementR As Double = Arc / (SampleSize - 1) * Inverse
        Dim MidpointX As Integer = VectorToPointX(m1x, Direction, Distance / 2)
        Dim MidpointY As Integer = VectorToPointY(m1y, Direction, Distance / 2)
        Dim DistanceToCP As Double = Distance / System.Math.Sin(Arc / 57.2958)
        Dim CentrePointX As Integer = VectorToPointX(MidpointX, Direction + 90 * Inverse, DistanceToCP)
        Dim CentrePointY As Integer = VectorToPointY(MidpointY, Direction + 90 * Inverse, DistanceToCP)
        Dim InitialDir As Double = ReturnDirection(CentrePointX, CentrePointY, m1x, m1y)
        Dim Radius As Integer = ReturnDistance(CentrePointX, CentrePointY, m1x, m1y)
        Dim Count As Integer = 0
        For Each a As Integer In pool
            MoveFromVector(a, CentrePointX, CentrePointY, InitialDir + IncrementR * Count, Radius)
            Count = Count + 1
        Next
    End Sub

    'Update Marker Appearance
    Private Sub LabelMarkers(ShowLabel As Boolean, LabelText As String, pool As Array)
        For Each a In pool
            If ShowLabel Then
                CheckNameTag(a - 1) = LabelText
            Else
                CheckNameTag(a - 1) = ""
            End If
        Next
        Refresh()
    End Sub
    Private Sub ColourMarkers(Colour As String, pool As Array)
        For Each a In pool
            CheckColour(a - 1) = Colour
        Next
        Refresh()
    End Sub

    'Mouse Click on Objects
    Private Sub Drag_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Marker1.MouseDown, Marker2.MouseDown, Marker4.MouseDown, Marker8.MouseDown, Marker7.MouseDown, Marker6.MouseDown, Marker5.MouseDown, Marker3.MouseDown, Point_2.MouseDown, Point_1.MouseDown, Marker9.MouseDown, Marker32.MouseDown, Marker31.MouseDown, Marker30.MouseDown, Marker29.MouseDown, Marker28.MouseDown, Marker27.MouseDown, Marker26.MouseDown, Marker25.MouseDown, Marker24.MouseDown, Marker23.MouseDown, Marker22.MouseDown, Marker21.MouseDown, Marker20.MouseDown, Marker19.MouseDown, Marker18.MouseDown, Marker17.MouseDown, Marker16.MouseDown, Marker15.MouseDown, Marker14.MouseDown, Marker13.MouseDown, Marker12.MouseDown, Marker11.MouseDown, Marker10.MouseDown
        MouseDragClick()
        Me.Refresh()
    End Sub
    Private Sub Drag_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Marker1.MouseMove, Marker2.MouseMove, Marker4.MouseMove, Marker8.MouseMove, Marker7.MouseMove, Marker6.MouseMove, Marker5.MouseMove, Marker3.MouseMove, Marker9.MouseMove, Marker32.MouseMove, Marker31.MouseMove, Marker30.MouseMove, Marker29.MouseMove, Marker28.MouseMove, Marker27.MouseMove, Marker26.MouseMove, Marker25.MouseMove, Marker24.MouseMove, Marker23.MouseMove, Marker22.MouseMove, Marker21.MouseMove, Marker20.MouseMove, Marker19.MouseMove, Marker18.MouseMove, Marker17.MouseMove, Marker16.MouseMove, Marker15.MouseMove, Marker14.MouseMove, Marker13.MouseMove, Marker12.MouseMove, Marker11.MouseMove, Marker10.MouseMove ' Point_2.MouseMove, Point_1.MouseMove
        MouseDrag(sender)
        DisplayX.Text = "X = " & CStr(MousePosition.X - Me.Left - BorderWidth) 'Tracks the position of the Mouse Relative to the Form
        DisplayY.Text = "Y = " & CStr(MousePosition.Y - Me.Top - TitlebarHeight - TopBanner.Height - BorderWidth)
    End Sub
    Private Sub Drag_Pointers(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Point_2.MouseMove, Point_1.MouseMove
        MouseDrag(sender)
        DisplayX.Text = "X = " & CStr(MousePosition.X - Me.Left - BorderWidth) 'Tracks the position of the Mouse Relative to the Form
        DisplayY.Text = "Y = " & CStr(MousePosition.Y - Me.Top - TitlebarHeight - TopBanner.Height - BorderWidth)
        If mdown Then
            Me.Refresh()
        End If
    End Sub
    Private Sub Drag_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Marker1.MouseUp, Marker2.MouseUp, Marker4.MouseUp, Marker8.MouseUp, Marker7.MouseUp, Marker6.MouseUp, Marker5.MouseUp, Marker3.MouseUp, Marker9.MouseUp, Marker32.MouseUp, Marker31.MouseUp, Marker30.MouseUp, Marker29.MouseUp, Marker28.MouseUp, Marker27.MouseUp, Marker26.MouseUp, Marker25.MouseUp, Marker24.MouseUp, Marker23.MouseUp, Marker22.MouseUp, Marker21.MouseUp, Marker20.MouseUp, Marker19.MouseUp, Marker18.MouseUp, Marker17.MouseUp, Marker16.MouseUp, Marker15.MouseUp, Marker14.MouseUp, Marker13.MouseUp, Marker12.MouseUp, Marker11.MouseUp, Marker10.MouseUp
        MouseDragUnclick()
        PositionCorrect(sender)
        Refresh()
    End Sub
    Private Sub DragUp_Pointers(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Point_2.MouseUp, Point_1.MouseUp
        MouseDragUnclick()
        PositionCorrect(sender)
        SelectedAreaValues()
        Refresh()
        UpdateImageSelection()
    End Sub
    Private Sub ClickMarker(sender As Object, e As EventArgs) Handles Marker1.Click, Marker9.MouseClick, Marker8.MouseClick, Marker7.MouseClick, Marker6.MouseClick, Marker5.MouseClick, Marker4.MouseClick, Marker32.MouseClick, Marker31.MouseClick, Marker30.MouseClick, Marker3.MouseClick, Marker29.MouseClick, Marker28.MouseClick, Marker27.MouseClick, Marker26.MouseClick, Marker25.MouseClick, Marker24.MouseClick, Marker23.MouseClick, Marker22.MouseClick, Marker21.MouseClick, Marker20.MouseClick, Marker2.MouseClick, Marker19.MouseClick, Marker18.MouseClick, Marker17.MouseClick, Marker16.MouseClick, Marker15.MouseClick, Marker14.MouseClick, Marker13.MouseClick, Marker12.MouseClick, Marker11.MouseClick, Marker10.MouseClick, Marker1.MouseClick
        If CheckClick Then
            Dim CurrentNumber As Integer = ReturnNumber(sender)
            Select Case ChooseValues.SelectedItem
                Case "Custom"
                    ReDim Preserve CustomPool(CustomPool.Length)
                    CustomPool(CustomPool.Length - 1) = CurrentNumber
                    DisplayCustomPool()
                    CurrentPool = CustomPool
                Case "Single"
                    SelectSingle.Value = CurrentNumber
                    CurrentPool = {SelectSingle.Value}
            End Select
        End If
    End Sub


    'Form Loading
    Private Sub Main_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        DisplayX.Text = "X = " & CStr(MousePosition.X - Me.Left - BorderWidth) 'Tracks the position of the Mouse Relative to the Form
        DisplayY.Text = "Y = " & CStr(MousePosition.Y - Me.Top - TitlebarHeight - TopBanner.Height - BorderWidth)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TotalMarkers = NumericUpDown3.Value
        AllValues()
        ResetUIMain()
        ResetUIValues()
        DisplayCustomPool()
        'Dim CurrentMarker As Object = Marker1
        'For a As Integer = 1 To TotalMarkers
        '    CurrentMarker = ReturnMarker(a)
        'Next
        ImageLeft = 0
        ImageTop = TopBanner.Height
        ImageWidth = Me.ClientSize.Width
        ImageHeight = Me.ClientSize.Height - TopBanner.Height
    End Sub
    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        TopBanner.Width = Me.ClientSize.Width
    End Sub

    'Update Marker Quantity
    Private Sub UpdateQuantity_Click(sender As Object, e As EventArgs) Handles QuantityButton.Click
        TotalMarkers = NumericUpDown3.Value
        UpdateQuantity(TotalMarkers)
    End Sub
    Public Sub UpdateQuantity(Num As Integer)
        For a As Integer = 1 To 32
            If Num >= a Then
                ReturnMarker(a).Visible = True
            End If
            If Num < a Then
                ReturnMarker(a).Visible = False
            End If
        Next
    End Sub

    'Do On Click
    Private Sub ClickMakeLine()
        'CreatePool()
        Dim Marker1x As Integer = Point_1.Location.X + (Point_1.Width / 2)
        Dim Marker1y As Integer = Point_1.Location.Y + (Point_1.Height / 4)
        Dim Marker2x As Integer = Point_2.Location.X + (Point_2.Width / 2)
        Dim Marker2y As Integer = Point_2.Location.Y + (Point_2.Height / 4)
        Dim OneOnOne As Boolean = OneOnOneCheck.Checked
        Dim Offset As Integer = OneOnOneOffset.Value
        MakeLine(Marker1x, Marker1y, Marker2x, Marker2y, CurrentPool, OneOnOne, Offset)
    End Sub
    Private Sub ClickMakeRows()
        If SelectRows.Value * SelectLength.Value <= CurrentPool.Length Then
            Dim Marker1x As Integer = Point_1.Location.X + (Point_1.Width / 2)
            Dim Marker1y As Integer = Point_1.Location.Y + (Point_1.Height / 4)
            Dim Marker2x As Integer = Point_2.Location.X + (Point_2.Width / 2)
            Dim Marker2y As Integer = Point_2.Location.Y + (Point_2.Height / 4)
            Dim Rows As Integer = SelectRows.Value
            Dim Length As Integer = SelectLength.Value
            MakeRows(Marker1x, Marker1y, Marker2x, Marker2y, Rows, Length, CurrentPool)
        Else
            MsgBox("There aren't enough markers being used")
        End If
    End Sub
    Private Sub ClickMakeCircle()
        'CreatePool()
        Dim Marker1x As Integer = Point_1.Location.X + (Point_1.Width / 2)
        Dim Marker1y As Integer = Point_1.Location.Y + (Point_1.Height / 4)
        Dim Marker2x As Integer = Point_2.Location.X + (Point_2.Width / 2)
        Dim Marker2y As Integer = Point_2.Location.Y + (Point_2.Height / 4)
        Dim Distance As Integer = ReturnDistance(Marker1x, Marker1y, Marker2x, Marker2y)
        Dim Direction As Integer = ReturnDirection(Marker1x, Marker1y, Marker2x, Marker2y)
        Dim Clockwise As Boolean = CheckDirection.Checked
        Dim Arc As Integer = SelectAngle.Value
        MakeCircle(Marker1x, Marker1y, Distance, Direction, CurrentPool, Clockwise, Arc)
    End Sub
    Private Sub ClickMakeCurve()
        'CreatePool()
        Dim Marker1x As Integer = Point_1.Location.X + (Point_1.Width / 2)
        Dim Marker1y As Integer = Point_1.Location.Y + (Point_1.Height / 4)
        Dim Marker2x As Integer = Point_2.Location.X + (Point_2.Width / 2)
        Dim Marker2y As Integer = Point_2.Location.Y + (Point_2.Height / 4)
        Dim Distance As Double = ReturnDistance(Marker1x, Marker1y, Marker2x, Marker2y)
        Dim Direction As Double = ReturnDirection(Marker1x, Marker1y, Marker2x, Marker2y)
        Dim Arc As Double = SelectAngle.Value
        Dim Clockwise As Boolean = CheckDirection.Checked
        MakeCurve(Marker1x, Marker1y, Distance, Direction, Arc, CurrentPool, Clockwise)
    End Sub
    Private Sub ClickLabelMarkers()
        'CreatePool()
        Dim ShowLabel As Boolean = ShowNameTagCheck.Checked
        Dim LabelName As String = WriteNameTag.Text
        LabelMarkers(ShowLabel, LabelName, CurrentPool)
    End Sub
    Private Sub ClickColourMarkers()
        'CreatePool()
        Dim Colour As String = SelectColour.SelectedItem
        ColourMarkers(Colour, CurrentPool)
    End Sub

    Private Sub RunFunction(sender As Object, e As EventArgs) Handles RunFunctionBtn.Click
        Select Case ChooseFunction.SelectedItem
            Case "Make Line"
                ClickMakeLine()
            Case "Make Circle"
                ClickMakeCircle()
            Case "Make Rows"
                'MsgBox("This one doesn't work yet")
                ClickMakeRows()
            Case "Make Curve"
                ClickMakeCurve()
            Case "Label Markers"
                ClickLabelMarkers()
            Case "Colour Markers"
                ClickColourMarkers()
            Case "Save To Text"
                SaveToTextForm.Show()
            Case "Save To Image"
                SaveToImageForm.Show()
        End Select
        Refresh()
    End Sub

    'Draw Functions
    Private Sub MarkerPaint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Marker1.Paint, Marker9.Paint, Marker8.Paint, Marker7.Paint, Marker6.Paint, Marker5.Paint, Marker4.Paint, Marker32.Paint, Marker31.Paint, Marker30.Paint, Marker3.Paint, Marker29.Paint, Marker28.Paint, Marker27.Paint, Marker26.Paint, Marker25.Paint, Marker24.Paint, Marker23.Paint, Marker22.Paint, Marker21.Paint, Marker20.Paint, Marker2.Paint, Marker19.Paint, Marker18.Paint, Marker17.Paint, Marker16.Paint, Marker15.Paint, Marker14.Paint, Marker13.Paint, Marker12.Paint, Marker11.Paint, Marker10.Paint
        Dim B_Marker = MarkerColor(sender)
        'Dim B_Black As New Drawing.SolidBrush(Color.Black)
        Dim dimensions As New Rectangle(0, 0, sender.Width, sender.Height)
        e.Graphics.FillEllipse(B_Marker, dimensions)
        Dim MarkerNumber As String = ReturnNumber(sender)
        Dim MarkerFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Bold)
        If ShowNumbersCheck.Checked Then
            Dim B_Number = NumberColour(sender)
            'Dim B_Red As New Drawing.SolidBrush(Color.Red)
            Dim MarkerFormat As New StringFormat
            MarkerFormat.Alignment = StringAlignment.Center
            MarkerFormat.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(MarkerNumber, MarkerFont, B_Number, dimensions, MarkerFormat)
        End If
        Select Case ChooseFunction.SelectedItem
            Case "Make Line", "Make Circle", "Make Curve", "Label Markers", "Colour Markers"
                If ShowHighlightedCheck.Checked Then
                    For Each a In CurrentPool
                        If ReturnNumber(sender) = a Then
                            Dim HighlightPen As New Drawing.Pen(Color.Aqua, 2)
                            'HighlightPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
                            e.Graphics.DrawEllipse(HighlightPen, dimensions)
                        End If
                    Next
                End If
        End Select
    End Sub
    Private Sub PointerPaint(sender As Object, e As PaintEventArgs) Handles Point_2.Paint, Point_1.Paint
        Dim P_Red As New Drawing.Pen(Color.Red)
        Dim dimensions As New Rectangle(0, 0, sender.Width, sender.Height)
        e.Graphics.DrawLine(P_Red, 0, 0, 16, 16)
        e.Graphics.DrawLine(P_Red, 0, 16, 16, 0)
        Dim MarkerNumber As String = ReturnNumber(sender)
        Dim MarkerFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Bold)
        Dim B_Red As New Drawing.SolidBrush(Color.Red)
        Dim MarkerFormat As New StringFormat
        MarkerFormat.Alignment = StringAlignment.Center
        MarkerFormat.LineAlignment = StringAlignment.Far
        e.Graphics.DrawString(MarkerNumber, MarkerFont, B_Red, dimensions, MarkerFormat)
    End Sub
    Private Sub ValuesBetween2UIPaint(sender As Object, e As PaintEventArgs) Handles ValuesBetween2Control.Paint
        Dim LabelFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
        Dim B_Black As New Drawing.SolidBrush(Color.Black)
        e.Graphics.DrawString("to", LabelFont, B_Black, 45, 10)
    End Sub
    Private Sub MakeRowsUIPaint(sender As Object, e As PaintEventArgs) Handles RowsControl.Paint
        Dim LabelFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
        Dim B_Black As New Drawing.SolidBrush(Color.Black)
        e.Graphics.DrawString("rows of", LabelFont, B_Black, 40, 10)
    End Sub
    Private Sub MakeLineUIPaint(sender As Object, e As PaintEventArgs) Handles LineControl.Paint
        Dim LabelFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
        Dim B_Black As New Drawing.SolidBrush(Color.Black)
        e.Graphics.DrawString("Offset", LabelFont, B_Black, 60, 35)
    End Sub
    Private Sub ValuesAlternatingUIPaint(sender As Object, e As PaintEventArgs) Handles ValuesAltermatingControl.Paint
        Dim LabelFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
        Dim B_Black As New Drawing.SolidBrush(Color.Black)
        e.Graphics.DrawString("Every", LabelFont, B_Black, 10, 10)
        e.Graphics.DrawString("beginning at", LabelFont, B_Black, 10, 40)
        e.Graphics.DrawString("for", LabelFont, B_Black, 10, 60)
    End Sub
    Private Sub ValuesSingleUIPaint(sender As Object, e As PaintEventArgs) Handles ValuesSingleControl.Paint
        Dim LabelFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
        Dim B_Black As New Drawing.SolidBrush(Color.Black)
        e.Graphics.DrawString("Choose Marker:", LabelFont, B_Black, 10, 10)
    End Sub
    'Private Sub CurveControlUIPaint(sender As Object, e As PaintEventArgs) Handles CurveControl.Paint
    '    Dim LabelFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
    '    Dim B_Black As New Drawing.SolidBrush(Color.Black)
    '    e.Graphics.DrawString("Arc Size", LabelFont, B_Black, 55, 35)
    'End Sub
    Private Sub SaveControlUIPaint(sender As Object, e As PaintEventArgs) Handles SaveControl.Paint
        Dim LabelFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
        Dim B_Black As New Drawing.SolidBrush(Color.Black)
        e.Graphics.DrawString("To File", LabelFont, B_Black, 20, 10)
    End Sub
    Private Sub BackPaint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        If ChooseValues.SelectedItem = "Select Area" And SelectAreaCheck.Checked And ShowPreviewCheck.Checked And ShowPointersCheck.Checked Then
            Dim Pointer1x As Integer = Point_1.Location.X + Point_1.Width / 2
            Dim Pointer1y As Integer = Point_1.Location.Y + Point_1.Height / 4
            Dim Pointer2x As Integer = Point_2.Location.X + Point_2.Width / 2
            Dim Pointer2y As Integer = Point_2.Location.Y + Point_2.Height / 4
            Dim P_Lime As New Drawing.Pen(Color.LimeGreen, 4)
            Dim p1x As Integer = System.Math.Min(Pointer1x, Pointer2x)
            Dim p1y As Integer = System.Math.Min(Pointer1y, Pointer2y)
            Dim p2x As Integer = System.Math.Abs(Pointer1x - Pointer2x)
            Dim p2y As Integer = System.Math.Abs(Pointer1y - Pointer2y)
            e.Graphics.DrawRectangle(P_Lime, p1x, p1y, p2x, p2y)
        Else
            Select Case ChooseFunction.SelectedItem
                Case "Make Line", "Make Circle", "Make Rows", "Make Curve", "Save To Image"
                    If ShowPreviewCheck.Checked And ShowPointersCheck.Checked Then
                        Dim Pointer1x As Integer = Point_1.Location.X + Point_1.Width / 2
                        Dim Pointer1y As Integer = Point_1.Location.Y + Point_1.Height / 4
                        Dim Pointer2x As Integer = Point_2.Location.X + Point_2.Width / 2
                        Dim Pointer2y As Integer = Point_2.Location.Y + Point_2.Height / 4
                        Dim P_Lime As New Drawing.Pen(Color.LimeGreen, 4)
                        Select Case ChooseFunction.SelectedItem
                            Case "Make Line"
                                e.Graphics.DrawLine(P_Lime, Pointer1x, Pointer1y, Pointer2x, Pointer2y)
                            Case "Make Circle"
                                Dim Radius As Integer = ReturnDistance(Pointer1x, Pointer1y, Pointer2x, Pointer2y)
                                Dim p1x As Integer = Pointer1x - Radius
                                Dim p1y As Integer = Pointer1y - Radius
                                Dim p2x As Integer = 2 * Radius
                                Dim p2y As Integer = 2 * Radius
                                e.Graphics.DrawEllipse(P_Lime, p1x, p1y, p2x, p2y)
                            Case "Make Rows"
                                Dim p1x As Integer = System.Math.Min(Pointer1x, Pointer2x)
                                Dim p1y As Integer = System.Math.Min(Pointer1y, Pointer2y)
                                Dim p2x As Integer = System.Math.Abs(Pointer1x - Pointer2x)
                                Dim p2y As Integer = System.Math.Abs(Pointer1y - Pointer2y)
                                e.Graphics.DrawRectangle(P_Lime, p1x, p1y, p2x, p2y)
                            Case "Make Curve"
                                Dim Arc As Integer = SelectAngle.Value
                                Dim Inverse As Integer = 1
                                If Not CheckDirection.Checked Then
                                    Inverse = -1
                                End If
                                Dim Distance As Integer = ReturnDistance(Pointer1x, Pointer1y, Pointer2x, Pointer2y)
                                If Distance = 0 Then
                                    Exit Sub
                                End If
                                Dim Direction As Integer = ReturnDirection(Pointer1x, Pointer1y, Pointer2x, Pointer2y)
                                Dim MidpointX As Integer = VectorToPointX(Pointer1x, Direction, Distance / 2)
                                Dim MidpointY As Integer = VectorToPointY(Pointer1y, Direction, Distance / 2)
                                Dim DistanceToCP As Double = Distance / System.Math.Sin(Arc / 57.2958)
                                Dim CentrePointX As Integer = VectorToPointX(MidpointX, Direction + 90 * Inverse, DistanceToCP)
                                Dim CentrePointY As Integer = VectorToPointY(MidpointY, Direction + 90 * Inverse, DistanceToCP)
                                Dim InitialDir As Integer = ReturnDirection(CentrePointX, CentrePointY, Pointer1x, Pointer1y)
                                Dim Radius As Integer = ReturnDistance(CentrePointX, CentrePointY, Pointer1x, Pointer1y)
                                Dim p1x As Integer = CentrePointX - Radius
                                Dim p1y As Integer = CentrePointY - Radius
                                Dim p2x As Integer = 2 * Radius
                                Dim p2y As Integer = 2 * Radius
                                e.Graphics.DrawArc(P_Lime, p1x, p1y, p2x, p2y, InitialDir - 90, Arc * Inverse)
                                'e.Graphics.DrawRectangle(P_Lime, p1x, p1y, p2x, p2y)
                                'e.Graphics.DrawLine(P_Lime, CentrePointX - 1, CentrePointY - 1, CentrePointX + 1, CentrePointY + 1)
                            Case "Save To Image"
                                If CheckImageUpdate.Checked Then
                                    Dim p1x As Integer = System.Math.Min(Pointer1x, Pointer2x)
                                    Dim p1y As Integer = System.Math.Min(Pointer1y, Pointer2y)
                                    Dim p2x As Integer = System.Math.Abs(Pointer1x - Pointer2x)
                                    Dim p2y As Integer = System.Math.Abs(Pointer1y - Pointer2y)
                                    e.Graphics.DrawRectangle(P_Lime, p1x, p1y, p2x, p2y)
                                End If
                        End Select
                    End If
            End Select
        End If
        If Not mdown Then
            Dim LabelFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
            Dim B_Black As New Drawing.SolidBrush(Color.Black)
            For a As Integer = 1 To TotalMarkers
                Dim CurrentNameTag As String = CheckNameTag(a - 1)
                Dim CurrentMarker As Object = ReturnMarker(a)
                Dim CurrentX As Integer = CurrentMarker.Location.X
                Dim CurrentY As Integer = CurrentMarker.Location.Y + CurrentMarker.Height
                e.Graphics.DrawString(CurrentNameTag, LabelFont, B_Black, CurrentX, CurrentY)
            Next
        End If
    End Sub

    'UI Placement
    Private Sub ResetUIMain()
        MoveTo(ValuesBetween2Control, 500, -500)
        MoveTo(RowsControl, 500, -500)
        MoveTo(CurveControl, 500, -500)
        MoveTo(LineControl, 500, -500)
        MoveTo(NameTagControl, 500, -500)
        MoveTo(ColourControl, 500, -500)
        MoveTo(SaveControl, 500, -500)
        SaveToTextForm.Close()
        MoveTo(SaveToImageControl, 500, -500)
        SaveToImageForm.Close()
        ValuesVisible(False)
    End Sub
    Private Sub ValuesVisible(a As Boolean)
        ChooseValues.Visible = a
        ValuesBetween2Control.Visible = a
        ValuesCustomControl.Visible = a
        ValuesAltermatingControl.Visible = a
        ValuesSingleControl.Visible = a
        ValuesSelectAreaControl.Visible = a
    End Sub
    Private Sub ResetUIValues()
        MoveTo(ValuesBetween2Control, 500, -500)
        MoveTo(ValuesCustomControl, 500, -500)
        MoveTo(ValuesAltermatingControl, 500, -500)
        MoveTo(ValuesSingleControl, 500, -500)
        MoveTo(ValuesSelectAreaControl, 500, -500)
    End Sub
    Private Sub ChooseFunctionUI(sender As Object, e As EventArgs) Handles ChooseFunction.SelectedIndexChanged
        ResetUIMain()
        Select Case ChooseFunction.SelectedItem
            Case "Make Line"
                MoveTo(LineControl, 600, 40)
                ValuesVisible(True)
            Case "Make Rows"
                MoveTo(RowsControl, 600, 50)
                ValuesVisible(True)
            Case "Make Circle"
                MoveTo(CurveControl, 600, 40)
                ValuesVisible(True)
                SelectAngle.Value = 360
            Case "Make Curve"
                MoveTo(CurveControl, 600, 40)
                ValuesVisible(True)
                SelectAngle.Value = 30
            Case "Label Markers"
                MoveTo(NameTagControl, 600, 40)
                ValuesVisible(True)
            Case "Colour Markers"
                MoveTo(ColourControl, 600, 0)
                ValuesVisible(True)
            Case "Save & Load From Files"
                MoveTo(SaveControl, 600, 0)
            Case "Save & Load From Text"
                SaveToTextForm.Show()
            Case "Save To Image"
                MoveTo(SaveToImageControl, 600, 40)
                SaveToImageForm.Show()
        End Select
        Refresh()
    End Sub
    Private Sub ChooseValuesUI(sender As Object, e As EventArgs) Handles ChooseValues.SelectedIndexChanged
        ResetUIValues()
        Select Case ChooseValues.SelectedItem
            Case "Between Points"
                MoveTo(ValuesBetween2Control, 400, 50)
                ValuesBetween2()
            Case "Custom"
                MoveTo(ValuesCustomControl, 400, 50)
                CurrentPool = CustomPool
            Case "Alternating"
                MoveTo(ValuesAltermatingControl, 400, 20)
                AlternatingValues()
            Case "Single"
                MoveTo(ValuesSingleControl, 400, 50)
                CurrentPool = {SelectSingle.Value}
            Case "All"
                AllValues()
            Case "Select Area"
                MoveTo(ValuesSelectAreaControl, 400, 50)
                SelectedAreaValues()
        End Select
        Refresh()
    End Sub


    'Select Values
    Private Sub ValuesBetween2() Handles ValuesBetween2From.ValueChanged, ValuesBetween2To.ValueChanged
        Array.Resize(CurrentPool, 0)
        For a As Integer = ValuesBetween2From.Value To ValuesBetween2To.Value
            ReDim Preserve CurrentPool(CurrentPool.Length)
            CurrentPool(CurrentPool.Length - 1) = a
        Next
        Refresh()
    End Sub
    Private Sub AlternatingValues() Handles SelectStart.ValueChanged, SelectFactor.ValueChanged, SelectCount.ValueChanged
        Array.Resize(CurrentPool, 0)
        For a As Integer = 0 To SelectCount.Value - 1
            ReDim Preserve CurrentPool(CurrentPool.Length)
            CurrentPool(CurrentPool.Length - 1) = SelectStart.Value + a * SelectFactor.Value
        Next
        Refresh()
    End Sub
    Private Sub AllValues()
        Array.Resize(CurrentPool, 0) 'Clears Array
        For a As Integer = 1 To TotalMarkers
            ReDim Preserve CurrentPool(CurrentPool.Length)
            CurrentPool(CurrentPool.Length - 1) = a 'Adds next value to the Array
        Next
        Refresh()
    End Sub
    Private Sub DisplayCustomPool()
        Dim ToDisplay As String = ""
        For Each a In CustomPool
            ToDisplay = ToDisplay & a & " " 'Adds Number and a space
        Next
        CustomPoolDisplay.Text = ToDisplay
        Refresh()
    End Sub
    Private Sub AddToCustomPool(sender As Object, e As EventArgs) Handles CustomAdd.Click
        ReDim Preserve CustomPool(CustomPool.Length)
        CustomPool(CustomPool.Length - 1) = SelectCustom.Value
        DisplayCustomPool()
        CurrentPool = CustomPool
    End Sub
    Private Sub RemoveFromCustomPool(sender As Object, e As EventArgs) Handles CustomRemove.Click
        If CustomPool.Length > 0 Then
            Array.Resize(CustomPool, CustomPool.Length - 1)
            DisplayCustomPool()
            CurrentPool = CustomPool
        End If
        Refresh()
    End Sub
    Private Sub ClearCustomPool(sender As Object, e As EventArgs) Handles CustomClear.Click
        Array.Resize(CustomPool, 0)
        DisplayCustomPool()
        CurrentPool = CustomPool
        Refresh()
    End Sub
    Private Sub SelectSingle_ValueChanged(sender As Object, e As EventArgs) Handles SelectSingle.ValueChanged
        CurrentPool = {SelectSingle.Value}
        Refresh()
    End Sub
    Private Sub DisplaySelectedArea()
        Dim ToDisplay As String = ""
        For Each a In CurrentPool
            ToDisplay = ToDisplay & a & " " 'Adds Number and a space
        Next
        SelectAreaDisplay.Text = ToDisplay
    End Sub
    Private Sub SelectedAreaValues()
        If ChooseValues.SelectedItem = "Select Area" And SelectAreaCheck.Checked Then
            Dim Pointer1x As Integer = Point_1.Location.X + Point_1.Width / 2
            Dim Pointer1y As Integer = Point_1.Location.Y + Point_1.Height / 4
            Dim Pointer2x As Integer = Point_2.Location.X + Point_2.Width / 2
            Dim Pointer2y As Integer = Point_2.Location.Y + Point_2.Height / 4
            Dim p1x As Integer = System.Math.Min(Pointer1x, Pointer2x)
            Dim p1y As Integer = System.Math.Min(Pointer1y, Pointer2y)
            Dim p2x As Integer = System.Math.Abs(Pointer1x - Pointer2x)
            Dim p2y As Integer = System.Math.Abs(Pointer1y - Pointer2y)
            Dim Container As New Rectangle(p1x, p1y, p2x, p2y)
            Array.Resize(CurrentPool, 0)
            For a As Integer = 1 To TotalMarkers
                Dim CurrentMarker As Object = ReturnMarker(a)
                Dim CurrentX As Integer = CurrentMarker.Location.X + CurrentMarker.Width / 2
                Dim CurrentY As Integer = CurrentMarker.Location.Y + CurrentMarker.Height / 2
                If Container.Contains(CurrentX, CurrentY) Then
                    ReDim Preserve CurrentPool(CurrentPool.Length)
                    CurrentPool(CurrentPool.Length - 1) = a
                End If
            Next
            DisplaySelectedArea()
            Refresh()
        End If

    End Sub

    'UI Refresh
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles ShowNameTagCheck.CheckedChanged
        WriteNameTag.Visible = ShowNameTagCheck.Checked
    End Sub
    Private Sub UpdatePreview(sender As Object, e As EventArgs) Handles ShowPointersCheck.CheckedChanged
        Point_1.Visible = ShowPointersCheck.Checked
        Point_2.Visible = ShowPointersCheck.Checked
        ShowPreviewCheck.Visible = ShowPointersCheck.Checked
        Refresh()
    End Sub
    Private Sub RefreshPaint(sender As Object, e As EventArgs) Handles SelectAngle.ValueChanged, CheckDirection.CheckedChanged, ShowPreviewCheck.CheckedChanged, ShowHighlightedCheck.CheckedChanged, ShowNumbersCheck.CheckedChanged 'SelectAreaCheck.CheckedChanged
        Refresh()
    End Sub
    Private Sub CheckBoxSelectArea(sender As Object, e As EventArgs) Handles SelectAreaCheck.CheckedChanged
        SelectedAreaValues()
        Refresh()
    End Sub
    Private Sub CheckBoxImageSelect(sender As Object, e As EventArgs) Handles CheckImageUpdate.CheckedChanged
        UpdateImageSelection()
        Refresh()
    End Sub

    'Save and Load
    Private Sub SaveToFile(sender As Object, e As EventArgs) Handles SaveFileButton.Click
        Dim save As New SaveFileDialog()
        'save = New SaveFileDialog()
        save.CreatePrompt = True
        save.Filter = "Text File | *.txt"
        Try
            If save.ShowDialog() = DialogResult.OK Then
                Dim toFile As System.IO.StreamWriter
                System.IO.File.WriteAllText(save.FileName, "")
                toFile = My.Computer.FileSystem.OpenTextFileWriter(save.FileName, True)
                toFile.WriteLine("Dance Pattern Layout ver 1.2 save file")
                toFile.WriteLine(TotalMarkers)
                For a As Integer = 1 To TotalMarkers
                    Dim CurrentMarker As Object = ReturnMarker(a)
                    Dim WriteX As String = CurrentMarker.Location.X + CurrentMarker.Width / 2
                    Dim WriteY As String = CurrentMarker.Location.Y + CurrentMarker.Height / 2
                    Dim WriteNameTag As String = CheckNameTag(a - 1)
                    If WriteNameTag = Nothing Then
                        WriteNameTag = "_None_"
                    End If
                    Dim WriteColour As String = CheckColour(a - 1)
                    toFile.WriteLine(WriteX & ", " & WriteY & ", " & WriteNameTag & ", " & WriteColour)
                Next
                toFile.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub LoadFromFile(sender As Object, e As EventArgs) Handles LoadFileButton.Click
        Dim load As OpenFileDialog
        load = New OpenFileDialog()
        load.Filter = "Text File | *.txt"
        Try
            If load.ShowDialog() = DialogResult.OK Then
                Dim fromFile As System.IO.StreamReader
                fromFile = My.Computer.FileSystem.OpenTextFileReader(load.FileName)
                Dim toRead As String = ""
                Dim SaveVersion As String = fromFile.ReadLine
                Dim SaveInfo As String = fromFile.ReadLine
                Select Case SaveVersion
                    Case "Dance Pattern Layout ver 1.2 save file"
                        TotalMarkers = SaveInfo
                        UpdateQuantity(TotalMarkers)
                        For a As Integer = 1 To TotalMarkers
                            Dim CurrentLine As String = fromFile.ReadLine()
                            Dim CurrentMarker As Object = ReturnMarker(a)
                            Dim Section As String() = CurrentLine.Split(New Char() {","c})
                            MoveMarkerTo(a, Section(0), Section(1))
                            Dim NameTagSaved As String = Trim(Section(2))
                            If NameTagSaved = "_None_" Then
                                NameTagSaved = ""
                            End If
                            CheckNameTag(a - 1) = NameTagSaved
                            CheckColour(a - 1) = Trim(Section(3))
                        Next
                        Refresh()
                    Case Else
                        MsgBox("This save file isn't working properly")
                End Select
                toRead = fromFile.ReadLine()

                fromFile.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub UpdateImageSelection()
        If CheckImageUpdate.Checked Then
            Dim Pointer1x As Integer = Point_1.Location.X + Point_1.Width / 2
            Dim Pointer1y As Integer = Point_1.Location.Y + Point_1.Height / 4
            Dim Pointer2x As Integer = Point_2.Location.X + Point_2.Width / 2
            Dim Pointer2y As Integer = Point_2.Location.Y + Point_2.Height / 4
            Dim P_Lime As New Drawing.Pen(Color.LimeGreen, 4)
            ImageLeft = System.Math.Min(Pointer1x, Pointer2x)
            ImageTop = System.Math.Min(Pointer1y, Pointer2y)
            ImageWidth = System.Math.Abs(Pointer1x - Pointer2x)
            ImageHeight = System.Math.Abs(Pointer1y - Pointer2y)
        Else
            ImageLeft = 0
            ImageTop = 0 + TopBanner.Height
            ImageWidth = Me.ClientSize.Width
            ImageHeight = Me.ClientSize.Height - TopBanner.Height
        End If
    End Sub

    'Private Sub SaveToText(sender As Object, e As EventArgs) Handles SaveTextButton.Click
    '    RichTextBox1.Text = "Dance Pattern Layout ver 1.2 save file" & vbCrLf
    '    RichTextBox1.Text = RichTextBox1.Text & TotalMarkers & vbCrLf
    '    For a As Integer = 1 To TotalMarkers
    '        Dim CurrentMarker As Object = ReturnMarker(a)
    '        Dim WriteX As String = CurrentMarker.Location.X + CurrentMarker.Width / 2
    '        Dim WriteY As String = CurrentMarker.Location.Y + CurrentMarker.Height / 2
    '        Dim WriteNameTag As String = CheckNameTag(a - 1)
    '        If WriteNameTag = Nothing Then
    '            WriteNameTag = "_None_"
    '        End If
    '        Dim WriteColour As String = CheckColour(a - 1)
    '        RichTextBox1.Text = RichTextBox1.Text & WriteX & ", " & WriteY & ", " & WriteNameTag & ", " & WriteColour & vbCrLf
    '    Next
    'End Sub
    'Private Sub LoadFromText(sender As Object, e As EventArgs) Handles LoadTextButton.Click
    '    If RichTextBox1.Lines.Length >= 3 Then
    '        Dim SaveVersion As String = RichTextBox1.Lines(0)
    '        Dim SaveInfo As String = RichTextBox1.Lines(1)
    '        Select Case SaveVersion
    '            Case "Dance Pattern Layout ver 1.2 save file"
    '                TotalMarkers = SaveInfo
    '                UpdateQuantity(TotalMarkers)
    '                For a As Integer = 1 To TotalMarkers
    '                    Dim CurrentLine As String = RichTextBox1.Lines(a + 1)
    '                    Dim CurrentMarker As Object = ReturnMarker(a)
    '                    Dim Section As String() = CurrentLine.Split(New Char() {","c})
    '                    MoveMarkerTo(a, Section(0), Section(1))
    '                    Dim NameTagSaved As String = Trim(Section(2))
    '                    If NameTagSaved = "_None_" Then
    '                        NameTagSaved = ""
    '                    End If
    '                    CheckNameTag(a - 1) = NameTagSaved
    '                    CheckColour(a - 1) = Trim(Section(3))
    '                Next
    '                Refresh()
    '            Case Else
    '                MsgBox("This save isn't working properly")
    '        End Select
    '    Else
    '        MsgBox("This isn't a valid save")
    '    End If
    'End Sub

    'Private Sub Form1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    'Dim f As EventArgs
    '    Select Case e.KeyData
    '        Case Keys.Enter
    '            'RunFunction(sender, f)
    '            MsgBox(":)")
    '    End Select
    'End Sub
End Class