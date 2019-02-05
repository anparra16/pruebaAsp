<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="prueba._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <%--checkbox div--%>
            <div class="col-6 col-md-4">                
                <div class="col text-center" >
                    <h2>Customers</h2>

                    <div class="col">
                    <div class="input-group">
                        <asp:TextBox runat="server" ID="Filter" Cssclass="form-control" OnTextChanged="Go_Click" AutoPostBack="true"  placeholder="Search for..."/>
                        <span class="input-group-btn">
                        <asp:Button runat="server" OnClick="Go_Click" Text="Go" Cssclass="btn btn-dark" />
                        </span>
                    </div><!-- /input-group -->
                    </div><!-- /.col -->
                    
                    <br />
                </div>
                <div class="col" style="overflow:auto;max-height:70vh";>
                    <input type="hidden" runat="server" id="hdnSelectedValue" />
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" AutoPostBack="True">
                    </asp:CheckBoxList>
                </div>    
            </div>
            <%--rigth side--%>

            <div class="col-12 col-md-8">
                <div class="row">
                    <div class="col" id="MainDetails" runat="server">
                        <div class="col text-center">
                            <h2>Customers you selected</h2>
                        </div>
                        <div class="row">
                            <div class="col-9 col-md-6">
                                <div class="col text-center">
                                    <h2>Primary</h2>
                                </div>
                                <br />
                                <input type="hidden" runat="server" id="id_primary" value='<%# DataBinder.Eval(Customer_data_primary.DataSource,"RecordNumber") %>'/>
                                <div class="col">
                                    <asp:Repeater ID="Customer_data_primary" runat="server">
                                        <ItemTemplate>
                                            <div class="container">
                                                <dl class="row">
                                                    <dt class="col-sm-6">Account Number:</dt>
                                                    <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "RecordNumber") %></dd>
                                                    <dt class="col-sm-6">First Name:</dt>
                                                    <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "FirstName") %></dd>
                                                    <dt class="col-sm-6">Last Name:</dt>
                                                    <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "LastName") %></dd>
                                                    <dt class="col-sm-6">Gender:</dt>
                                                    <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "Gender") %></dd>
                                                    <dt class="col-sm-6">Street Name:</dt>
                                                    <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "StreetName") %></dd>
                                                    <dt class="col-sm-6">State</dt>
                                                    <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "City") %></dd>
                                                    <dt class="col-sm-6">City</dt>
                                                    <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "State") %></dd>
                                                </dl>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                            </div>
                            <div class="col-9 col-md-6">
                                <div class="col text-center">
                                    <h2>Secondary</h2>
                                </div>
                                <br />
                                <input type="hidden" runat="server" id="id_secondary" value='<%# DataBinder.Eval(Customer_data_secondary.DataSource,"RecordNumber") %>'/>
                                <asp:Repeater ID="Customer_data_secondary" runat="server">
                                    <ItemTemplate>
                                        <div class="container">
                                            <dl class="row">
                                                <dt class="col-sm-6">Account Number:</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "RecordNumber") %></dd>
                                                <dt class="col-sm-6">First Name:</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "FirstName") %></dd>
                                                <dt class="col-sm-6">Last Name:</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "LastName") %></dd>
                                                <dt class="col-sm-6">Gender:</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "Gender") %></dd>
                                                <dt class="col-sm-6">Street Name:</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "StreetName") %></dd>
                                                <dt class="col-sm-6">State</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "City") %></dd>
                                                <dt class="col-sm-6">City</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "State") %></dd>
                                            </dl>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    

                    <div class="col"  id="ConfirmData" runat="server">
                        <div class="col text-center">
                            <h2>Confirm data</h2>
                            <h6>Both addresses will be combined</h6>
                        </div>
                        <br />
                        <div class="row text-center">
                            <div class="col" style="display: inline-block;">


                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="FirstName" Cssclass="col-md-2 control-label">First Name</asp:Label>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="FirstName" Text='<%# DataBinder.Eval(Customer_data_primary.DataSource,"FirstName") %>' Cssclass="form-control" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                                                Cssclass="text-danger" ErrorMessage="First Name is Required." />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="LastName" Cssclass="col-md-2 control-label">Last Name</asp:Label>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="LastName" Cssclass="form-control" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                                                Cssclass="text-danger" ErrorMessage="Last Name is Required." />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="Gender" Cssclass="col-md-2 control-label">Gender</asp:Label>
                                        <div class="col-md-10">
                                            <asp:ListBox runat="server" ID="Gender" Cssclass="form-control" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Gender"
                                                Cssclass="text-danger" ErrorMessage="Gender is Required." />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-offset-2 col-md-10">
                                            <asp:Button runat="server" OnClick="Merge_Click" Text="Merge" Cssclass="btn btn-dark" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                

                <% if (AfterAdd)
                    { %>

                <div class="row">
                    <div class="col">
                        <div class="col text-center">
                            <h2>Customers combined</h2>
                        </div>
                        <br />
                        <div class="container-fluid">
                            <div class="col-md-8" style="float: none; margin: 0 auto;">
                                <asp:Repeater ID="Customer_data_result" runat="server">
                                    <ItemTemplate>
                                        <div class="container">
                                            <dl class="row">
                                                <dt class="col-sm-6">Account Number:</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "RecordNumber") %></dd>
                                                <dt class="col-sm-6">First Name:</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "FirstName") %></dd>
                                                <dt class="col-sm-6">Last Name:</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "LastName") %></dd>
                                                <dt class="col-sm-6">Gender:</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "Gender") %></dd>
                                                <dt class="col-sm-6">Street Name:</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "StreetName") %></dd>
                                                <dt class="col-sm-6">State</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "City") %></dd>
                                                <dt class="col-sm-6">City</dt>
                                                <dd class="col-sm-6"><%# DataBinder.Eval(Container.DataItem, "State") %></dd>
                                            </dl>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>

                <% } %>
            </div>
        </div>
    </div>

</asp:Content>
