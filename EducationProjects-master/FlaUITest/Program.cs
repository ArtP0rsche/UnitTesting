using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

var app = Application.Launch("C:\\Temp\\ispp21\\EducationProjects-master\\AuthApp\\bin\\Debug\\net8.0-windows\\AuthApp.exe");
var automation = new UIA3Automation();

var window = app.GetMainWindow(automation);
var regButton = window.FindFirstDescendant(rb => rb.ByText("Зарегистрироваться")).AsButton();
regButton.Click();
window.FindFirstDescendant(ok => ok.ByAutomationId("2")).Click();

var regLoginBox = window.FindFirstDescendant(rlb => rlb.ByAutomationId("regLoginBox")).AsTextBox();
regLoginBox.Enter("admin");
var regPasswordBox = window.FindFirstDescendant(rpb => rpb.ByAutomationId("regPasswordBox")).AsTextBox();
regPasswordBox.Enter("QwErTy1!");
var confirmationPasswordBox = window.FindFirstDescendant(cpb => cpb.ByAutomationId("confirmPasswordBox")).AsTextBox();
confirmationPasswordBox.Enter("QwerTy1!");
regButton.Click();
window.FindFirstDescendant(ok => ok.ByAutomationId("2")).Click();

app.Close();