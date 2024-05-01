<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mmaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mmaster))
        Me.b_cr = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.b_close = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'b_cr
        '
        Me.b_cr.Location = New System.Drawing.Point(327, 56)
        Me.b_cr.Name = "b_cr"
        Me.b_cr.Size = New System.Drawing.Size(75, 23)
        Me.b_cr.TabIndex = 0
        Me.b_cr.Text = "Create"
        Me.b_cr.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(58, 56)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(263, 20)
        Me.TextBox1.TabIndex = 1
        '
        'b_close
        '
        Me.b_close.Location = New System.Drawing.Point(327, 85)
        Me.b_close.Name = "b_close"
        Me.b_close.Size = New System.Drawing.Size(75, 23)
        Me.b_close.TabIndex = 2
        Me.b_close.Text = "Close"
        Me.b_close.UseVisualStyleBackColor = True
        '
        'mmaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(688, 307)
        Me.Controls.Add(Me.b_close)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.b_cr)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "mmaster"
        Me.Text = "Material Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents b_cr As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents b_close As System.Windows.Forms.Button
End Class
