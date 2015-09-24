﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DayConsumptionReport.aspx.cs" Inherits="CustomStatisticalReport.Web.UI_CustomStatisticalReport.DayConsumptionReport" %>

<%@ Register Src="../UI_WebUserControls/OrganizationSelector/OrganisationTree.ascx" TagName="OrganisationTree" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>能耗日报表</title>
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtIcon.css" />
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtCss.css" />

    <script type="text/javascript" src="/lib/ealib/jquery.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/jquery.easyui.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/easyui-lang-zh_CN.js" charset="utf-8"></script>

    <script type="text/javascript" src="/lib/ealib/extend/jquery.PrintArea.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/extend/jquery.jqprint.js" charset="utf-8"></script>

    <script type="text/javascript" src="js/page/DayConsumptionReport.js" charset="utf-8"></script>
</head>
<body>
    <div class="easyui-layout" data-options="fit:true,border:false">
        <div data-options="region:'west',split:true" style="width: 230px;">
            <uc1:OrganisationTree ID="OrganisationTree_ProductionLine" runat="server" />
        </div>
        <div data-options="region:'center', border:false">
            <div id="toolbar_ReportTable" style="display: none;">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>组织机构：</td>
                                    <td>
                                        <input id="TextBox_OrganizationName" class="easyui-textbox" style="width: 180px;" readonly="true" /><input id="organizationId" readonly="true" style="display: none;" /></td>
                                    <td>时间：</td>
                                    <td>
                                        <input id="datetime" type="text" class="easyui-datebox" style="width: 120px;" required="required" /></td>
                                    <td><a href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:'icon-search',plain:true"
                                        onclick="QueryReportFun();">查询</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload', plain:true" onclick="RefreshFun();">刷新</a>
                                    </td>
                                    <td>
                                        <div class="datagrid-btn-separator"></div>
                                    </td>
                                    <td><a href="#" class="easyui-linkbutton" data-options="iconCls:'ext-icon-page_white_excel',plain:true" onclick="ExportFileFun();">导出</a>
                                    </td>
                                    <td><a href="#" class="easyui-linkbutton" data-options="iconCls:'ext-icon-printer',plain:true" onclick="PrintFileFun();">打印</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <table id="TreeGrid_ReportTable" data-options="fit:true,border:false">
            </table>
        </div>
    </div>
    <form id="formMain" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
