<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SaveToTextForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SaveBox = New System.Windows.Forms.RichTextBox()
        Me.LoadTextButton = New System.Windows.Forms.Button()
        Me.SaveTextButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'SaveBox
        '
        Me.SaveBox.Location = New System.Drawing.Point(12, 12)
        Me.SaveBox.Name = "SaveBox"
        Me.SaveBox.Size = New System.Drawing.Size(195, 233)
        Me.SaveBox.TabIndex = 141
        Me.SaveBox.Text = ""
        '
        'LoadTextButton
        '
        Me.LoadTextButton.Location = New System.Drawing.Point(226, 46)
        Me.LoadTextButton.Name = "LoadTextButton"
        Me.LoadTextButton.Size = New System.Drawing.Size(60, 28)
        Me.LoadTextButton.TabIndex = 138
        Me.LoadTextButton.Text = "Load"
        Me.LoadTextButton.UseVisualStyleBackColor = True
        '
        'SaveTextButton
        '
        Me.SaveTextButton.Location = New System.Drawing.Point(226, 12)
        Me.SaveTextButton.Name = "SaveTextButton"
        Me.SaveTextButton.Size = New System.Drawing.Size(60, 28)
        Me.SaveTextButton.TabIndex = 137
        Me.SaveTextButton.Text = "Save"
        Me.SaveTextButton.UseVisualStyleBackColor = True
        '
        'SaveToTextForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(306, 256)
        Me.Controls.Add(Me.LoadTextButton)
        Me.Controls.Add(Me.SaveTextButton)
        Me.Controls.Add(Me.SaveBox)
        Me.Name = "SaveToTextForm"
        Me.Text = "Save To Text"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SaveBox As RichTextBox
    Friend WithEvents LoadTextButton As Button
    Friend WithEvents SaveTextButton As Button
End Class
