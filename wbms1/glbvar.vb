Module glbvar
    Public cmpcd As String
    Public divcd As String
    Public inout As String
    Public WERKS As String 'Plant
    Public LGORT As String 'Storage location
    Public VSTEL As String 'Shipping plant
    Public VKORG As String 'Sales Organisation
    Public VTWEG As String 'Distribution  Channel
    Public EKORG As String 'Purchasing Organisation 
    Public EKGRP As String 'Purchasing Group
    Public BUKRS As String 'Company Code
    Public SPART As String 'Division
    Public INDTYPE As String
    Public PRINT As String
    Public SERVER As String
    Public CLIENT As String
    Public SYSID As String
    Public VANLOC As String
    Public SSLGORT As String
    Public vyrcd As Integer
    Public vintdocno As Integer
    Public grpdivcd As String = "HOD"
    Public vtktno As Integer
    Public vntwt, vfwt, vswt As Integer
    Public ptyp As String
    Public itmalloc As Boolean = False
    Public gmultival As Boolean = False
    Public userid As String
    Public pindocn() As Integer
    Public ptktno() As Decimal
    Public prtktno() As Long
    Public pino() As String
    Public intiem() As Integer
    Public itmcde() As String
    Public itemdes() As String
    Public pqty() As Decimal
    Public pperc() As Decimal
    Public preco() As Decimal
    Public pfswt() As Decimal
    Public pscwt() As Decimal
    Public ppricekg() As Decimal
    Public prate() As Decimal
    Public pitem() As Decimal
    Public ppackded() As Decimal
    Public gded As Decimal = 0
    Public gmded As Decimal = 0
    Public pmultided() As Decimal
    Public gnetqty() As Decimal
    Public gactqty() As Decimal
    Public gvalue() As Decimal
    Public pomprice() As Decimal
    Public gomrate() As Decimal
    Public gsvalue() As Decimal
    Public gdate() As Date
    Public pvencode() As String
    Public pvendesc() As String
    Public pdcode() As String
    Public pdname() As String
    Public psapdoccode() As String
    Public psapdocdesc() As String
    Public odp() As Decimal
    Public thickp() As Decimal
    Public lengthp() As Decimal
    Public pipenop() As String
    Public fwtp() As Decimal
    Public swtp() As Decimal
    Public temp_suppcode As String
    Public temp_suppdesc As String
    Public temp_prsuppcode As String
    Public temp_prsuppdesc As String
    Public temp_crsuppcode As String
    Public temp_crsuppdesc As String
    Public temp_itemcode As String
    Public temp_itemdesc As String
    Public temp_fritemcode As String
    Public temp_fritemdesc As String
    Public temp_drcode As String
    Public temp_drdesc As String
    Public temp_cdrcode As String
    Public temp_cdrdesc As String
    Public temp_doctype As String
    Public temp_docdesc As String
    Public temp_omsledcode As String
    Public temp_omsleddesc As String
    Public temp_buycode As String
    Public temp_buydesc As String
    Public scaletype As String
    Public multkt As String
    Public sapdocmulti As String
    Public p_DATEIN() As Date
    Public p_dateout() As Date
    Public p_timein() As String
    Public p_timeout() As String
    Public p_numberofpcs() As Integer
    Public p_labourcharges() As Decimal
    Public p_penalty() As Decimal
    Public p_eqpchrgs() As Decimal
    Public p_transp() As Decimal
    Public p_cons_sen_branch() As Decimal
    Public p_orderno() As Decimal
    Public p_dsno() As Decimal
    Public p_asno() As Decimal
    Public p_IBDSNO() As Decimal
    Public p_ccic() As String
    Public p_vehicleno() As String
    Public p_oth_ven_cust() As String
    Public p_comments() As String
    Public p_DRIVERNAM() As String
    Public p_dcode() As String
    Public p_buyer() As String
    Public p_mqty() As Decimal ' for mix
    Public p_mitem() As Decimal ' for mix
    Public p_mpono() As String ' for mix
    Public p_mcomflg() As String ' for mix
    Public multdocno As Integer
    Public multtktno As Integer
    Public multinout As String
    Public gomcustcode() As String
    Public gomcustname() As String
    Public gomcusttkt() As String
    Public gomcustdate() As Date
    Public gcusttype() As String
    Public gtypecode() As String
    Public gtypecatg_pt() As String
    Public guom() As String
    Public gmixpo() As String
    Public gwerks() As String
    Public gdoccode As String
    Public gcompname As String
    Public gcompnamegp() As String
    Public ggpremarks() As String
    Public gsapordno As String
    Public gsapdocno As String
    Public gsapinvno As String
    Public mix As Boolean = False
    Public giinvdocno() As Decimal
    Public giINVSLNO() As Decimal
    Public giSCALE() As String
    Public giINTDOCNO() As Decimal
    Public giTICKETNO() As Decimal
    Public giSLEDCODE() As String
    Public giSLEDDESC() As String
    Public giSLNO() As Decimal
    Public giITEMCODE() As String
    Public giITEMDESC() As String
    Public giDATEOUT() As Date
    Public giPOSTDATE() As Date
    Public giFIRSTQTY() As Decimal
    Public giSECONDQTY() As Decimal
    Public giQTY() As Decimal
    Public giPRICETON() As Decimal
    Public giRATE() As Decimal
    Public giTOTAL_PRICE() As Decimal
    Public giVBELNS() As String
    Public giVBELND() As String
    Public giVBELNI() As String
    Public sscomments() As String
    Public pslnotr() As Decimal
    Public temp_gsuppcode As String
    Public temp_gsuppdesc As String
    Public pdocdatetr() As Date
    Public ptrailernotr() As String
    Public ptrailer_codetr() As String
    Public psledcodetr() As String
    Public psleddesctr() As String
    Public proutetr() As String
    Public pdrivernotr() As String
    Public pdriver_nametr() As String
    Public premarks() As String
    Public pnooftripstr() As Decimal
    Public ptripratetr() As Decimal
    Public pnetamounttr() As Decimal
    Public gitrcharge() As Decimal
    Public gipenalty() As Decimal
    Public gimacharge() As Decimal
    Public gilabcharge() As Decimal
    Public gbcode As String
    Public glgort() As String
    Public PRSLEDCODE() As String
    Public PRSLEDDESC() As String
    Public PRSUPPCODE() As String
    Public PRSUPPDESC() As String
    Public g_vendor As String
    Public g_driver As String
    Public g_pono As String
    Public g_itmno As String
    Public g_printmat As String
    Public g_gpono As String
    Public g_gitmno As String
    Public g_gform As String
    Public g_refno As String
    Public rnitemcode() As String
    Public rnitemdesc() As String
    Public rndate() As Date
    Public rnpurqty() As Decimal
    Public rnpurded() As Decimal
    Public rnsalqty() As Decimal
    Public rnsalded() As Decimal
    Public rnrecqty() As Decimal
    Public rnrecded() As Decimal
    Public rndiff() As Decimal
End Module
