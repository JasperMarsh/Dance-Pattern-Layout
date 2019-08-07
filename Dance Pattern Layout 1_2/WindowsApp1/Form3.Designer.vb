<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SaveToImageForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PrintableBuild = New System.Windows.Forms.PictureBox()
        Me.UpdateButton = New System.Windows.Forms.Button()
        Me.ShowNumbersCheck = New System.Windows.Forms.CheckBox()
        Me.ShowLabelsCheck = New System.Windows.Forms.CheckBox()
        Me.ColoursCheck = New System.Windows.Forms.CheckBox()
        Me.SaveImageButton = New System.Windows.Forms.Button()
        CType(Me.PrintableBuild, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PrintableBuild
        '
        Me.PrintableBuild.BackColor = System.Drawing.Color.White
        Me.PrintableBuild.Location = New System.Drawing.Point(138, 12)
        Me.PrintableBuild.Name = "PrintableBuild"
        Me.PrintableBuild.Size = New System.Drawing.Size(850, 501)
        Me.PrintableBuild.TabIndex = 0
        Me.PrintableBuild.TabStop = False
        '
        'UpdateButton
        '
        Me.UpdateButton.Location = New System.Drawing.Point(12, 12)
        Me.UpdateButton.Name = "UpdateButton"
        Me.UpdateButton.Size = New System.Drawing.Size(88, 52)
        Me.UpdateButton.TabIndex = 1
        Me.UpdateButton.Text = "Update"
        Me.UpdateButton.UseVisualStyleBackColor = True
        '
        'ShowNumbersCheck
        '
        Me.ShowNumbersCheck.AutoSize = True
        Me.ShowNumbersCheck.Checked = True
        Me.ShowNumbersCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowNumbersCheck.Location = New System.Drawing.Point(12, 70)
        Me.ShowNumbersCheck.Name = "ShowNumbersCheck"
        Me.ShowNumbersCheck.Size = New System.Drawing.Size(98, 17)
        Me.ShowNumbersCheck.TabIndex = 2
        Me.ShowNumbersCheck.Text = "Show Numbers"
        Me.ShowNumbersCheck.UseVisualStyleBackColor = True
        '
        'ShowLabelsCheck
        '
        Me.ShowLabelsCheck.AutoSize = True
        Me.ShowLabelsCheck.Checked = True
        Me.ShowLabelsCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowLabelsCheck.Location = New System.Drawing.Point(12, 93)
        Me.ShowLabelsCheck.Name = "ShowLabelsCheck"
        Me.ShowLabelsCheck.Size = New System.Drawing.Size(87, 17)
        Me.ShowLabelsCheck.TabIndex = 3
        Me.ShowLabelsCheck.Text = "Show Labels"
        Me.ShowLabelsCheck.UseVisualStyleBackColor = True
        '
        'ColoursCheck
        '
        Me.ColoursCheck.AutoSize = True
        Me.ColoursCheck.Checked = True
        Me.ColoursCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ColoursCheck.Location = New System.Drawing.Point(12, 116)
        Me.ColoursCheck.Name = "ColoursCheck"
        Me.ColoursCheck.Size = New System.Drawing.Size(61, 17)
        Me.ColoursCheck.TabIndex = 4
        Me.ColoursCheck.Text = "Colours"
        Me.ColoursCheck.UseVisualStyleBackColor = True
        '
        'SaveImageButton
        '
        Me.SaveImageButton.Location = New System.Drawing.Point(12, 156)
        Me.SaveImageButton.Name = "SaveImageButton"
        Me.SaveImageButton.Size = New System.Drawing.Size(88, 52)
        Me.SaveImageButton.TabIndex = 5
        Me.SaveImageButton.Text = "Save Image"
        Me.SaveImageButton.UseVisualStyleBackColor = True
        '
        'SaveToImageForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 557)
        Me.Controls.Add(Me.SaveImageButton)
        Me.Controls.Add(Me.ColoursCheck)
        Me.Controls.Add(Me.ShowLabelsCheck)
        Me.Controls.Add(Me.ShowNumbersCheck)
        Me.Controls.Add(Me.UpdateButton)
        Me.Controls.Add(Me.PrintableBuild)
        Me.MinimumSize = New System.Drawing.Size(0, 260)
        Me.Name = "SaveToImageForm"
        Me.Text = "Save To Image"
        CType(Me.PrintableBuild, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PrintableBuild As PictureBox
    Friend WithEvents UpdateButton As Button
    Friend WithEvents ShowNumbersCheck As CheckBox
    Friend WithEvents ShowLabelsCheck As CheckBox
    Friend WithEvents ColoursCheck As CheckBox
    Friend WithEvents SaveImageButton As Button
End Class
