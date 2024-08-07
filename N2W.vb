﻿Public Class N2W
#Region "Generated by IDE"
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return MyBase.GetHashCode()
    End Function

#End Region

    Private Function GetTens(TensText As String) As String
        ' Converts a number from 10 to 99 into text.
        GetTens = vbNullString
        If Val(Left(TensText, 1)) = 1 Then ' If value between 10-19...
            Select Case Val(TensText)
                Case 10 : GetTens = "Ten"
                Case 11 : GetTens = "Eleven"
                Case 12 : GetTens = "Twelve"
                Case 13 : GetTens = "Thirteen"
                Case 14 : GetTens = "Fourteen"
                Case 15 : GetTens = "Fifteen"
                Case 16 : GetTens = "Sixteen"
                Case 17 : GetTens = "Seventeen"
                Case 18 : GetTens = "Eighteen"
                Case 19 : GetTens = "Nineteen"
            End Select
        Else ' If value between 20-99...
            Select Case Val(Left(TensText, 1))
                Case 2 : GetTens = "Twenty"
                Case 3 : GetTens = "Thirty"
                Case 4 : GetTens = "Forty"
                Case 5 : GetTens = "Fifty"
                Case 6 : GetTens = "Sixty"
                Case 7 : GetTens = "Seventy"
                Case 8 : GetTens = "Eighty"
                Case 9 : GetTens = "Ninety"
            End Select
            Dim tmp As String = GetDigit(Right(TensText, 1)) ' Retrieve ones place.
            If tmp <> vbNullString Then
                GetTens &= $"-{tmp}"
            End If
            'GetTens &= GetDigit(Right(TensText, 1)) ' Retrieve ones place.
        End If
        Return GetTens
    End Function

    Private Function GetDigit(Digit) As String
        Select Case Val(Digit)
            Case 1 : GetDigit = " One"
            Case 2 : GetDigit = " Two"
            Case 3 : GetDigit = " Three"
            Case 4 : GetDigit = " Four"
            Case 5 : GetDigit = " Five"
            Case 6 : GetDigit = " Six"
            Case 7 : GetDigit = " Seven"
            Case 8 : GetDigit = " Eight"
            Case 9 : GetDigit = " Nine"
            Case Else : GetDigit = vbNullString
        End Select
        Return GetDigit
    End Function

    Private Function GetHundreds(MyNumber As String) As String
        ' Converts a number from 100-999 into text
        GetHundreds = vbNullString
        Dim Result As String = vbNullString
        If Val(MyNumber) = 0 Then Exit Function
        MyNumber = Right("000" & MyNumber, 3)
        ' Convert the hundreds place.
        If Mid(MyNumber, 1, 1) <> "0" Then
            Result = GetDigit(Mid(MyNumber, 1, 1)) & " Hundred "
        End If
        ' Convert the tens and ones place.
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result &= GetTens(Mid(MyNumber, 2))
        Else
            Result &= GetDigit(Mid(MyNumber, 3))
        End If
        GetHundreds = Result
    End Function

    Public Function NumberToWords(MyNumber As String) As String
        Dim Pesos As String = vbNullString
        Dim Cents As String = vbNullString
        Dim RightMost As String
        Dim Place(9) As String
        Place(2) = " Thousand"
        Place(3) = " Million"
        Place(4) = " Billion"
        Place(5) = " Trillion"
        ' String representation of amount.
        MyNumber = Trim(MyNumber)
        ' Position of decimal place 0 if none.
        Dim DecimalPlace As Integer = InStr(MyNumber, ".")
        ' Convert cents and set MyNumber to dollar amount.
        If DecimalPlace > 0 Then
            Cents = Left(Mid(MyNumber, DecimalPlace + 1) & "00", 3)
            RightMost = Right(Cents, 1)
            If CInt(RightMost) >= 5 Then Cents = CStr(CInt(Cents) + 10)
            Cents = Left(Cents, 2)
            Cents = " AND " & Cents & "/100 ONLY"
            MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
        End If
        Dim Count As Integer = 1
        Do While MyNumber <> vbNullString
            Dim Temp As String = GetHundreds(Right(MyNumber, 3))
            If Temp <> vbNullString Then Pesos = Temp & Place(Count) & Pesos
            If Len(MyNumber) > 3 Then
                MyNumber = Left(MyNumber, Len(MyNumber) - 3)
            Else
                MyNumber = vbNullString
            End If
            Count += 1
        Loop
        Select Case Pesos
            Case "" : Pesos = "No Pesos"
            Case "One" : Pesos = "One Pesos"
            Case Else : Pesos &= " Pesos"
        End Select
        If Cents = vbNullString Then
            NumberToWords = UCase(Pesos) & " ONLY"
        Else
            NumberToWords = UCase(Pesos & Cents)
        End If
    End Function
End Class
