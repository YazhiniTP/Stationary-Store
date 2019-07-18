<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SA47_Team12_StationeryStore._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container" style="width: 100%; height: 550px;">
        <div id="myCarousel" class="carousel slide" data-ride="carousel">
            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#myCarousel" data-slide-to="1"></li>
                <li data-target="#myCarousel" data-slide-to="2"></li>
                <li data-target="#myCarousel" data-slide-to="3"></li>
                <li data-target="#myCarousel" data-slide-to="4"></li>
                <li data-target="#myCarousel" data-slide-to="5"></li>
                <li data-target="#myCarousel" data-slide-to="6"></li>
            </ol>

            <!-- Wrapper for slides -->
            <div class="carousel-inner" style="width: 100%; height: 550px;">

                <div class="item active">
                    <img src="/Images/Book.jpg" alt="Book">
                    <div class="carousel-caption">
                        <h3>Book</h3>
                        <p>Book</p>
                    </div>
                </div>

                <div class="item">
                    <img src="/Images/Pencils.jpg" alt="Pencils">
                    <div class="carousel-caption">
                        <h3>Pencils</h3>
                        <p>Pencils</p>
                    </div>
                </div>

                <div class="item">
                    <img src="/Images/Pen.jpg" alt="Writing Pen">
                    <div class="carousel-caption">
                        <h3>Pen</h3>
                        <p>Writing</p>
                    </div>
                </div>

                <div class="item">
                    <img src="/Images/Note1.jpg" alt="Files">
                    <div class="carousel-caption">
                        <h3>Note</h3>
                        <p>Note</p>
                    </div>
                </div>

                <div class="item">
                    <img src="/Images/Files.jpg" alt="Files">
                    <div class="carousel-caption">
                        <h3>Files</h3>
                        <p>Files</p>
                    </div>
                </div>

                <div class="item">
                    <img src="/Images/Note.jpg" alt="Files">
                    <div class="carousel-caption">
                        <h3>Note</h3>
                        <p>Note</p>
                    </div>
                </div>

                <div class="item">
                    <img src="/Images/Files1.jpg" alt="Files">
                    <div class="carousel-caption">
                        <h3>Files</h3>
                        <p>Files</p>
                    </div>
                </div>

            </div>

            <!-- Left and right controls -->
            <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#myCarousel" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>
    <br />
    <br />
    <div>
        <asp:TextBox ID="SearchTextBox" runat="server" placeholder=" Type to search here..." Width="550px" Height="30px"></asp:TextBox>
        &nbsp;&nbsp;
            <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" CssClass="btn btn-primary" Width="76px" />
    </div>
    <br />
    <div>
        <asp:GridView ID="ItemListGridView" runat="server" CssClass="table table-striped table-bordered" DataKeyNames="ItemID"
            EmptyDataText="There are no items to display." ShowHeaderWhenEmpty="True"
            AllowPaging="True" OnPageIndexChanging="ItemListGridView_PageIndexChanging" PageSize="10"
            GridLines="None" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Item_Description" HeaderText="Item Description" />
                <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost" />
                <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit Of Measure" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#284775" ForeColor="White" />
            <HeaderStyle BackColor="#284775" ForeColor="White" />
            <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
        </asp:GridView>
    </div>

</asp:Content>
