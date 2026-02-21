using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace SMSWebApi.Services
{
    public class SMSBackgroundService : BackgroundService
    {
        private readonly ColaService _colaService;
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private bool _chromeInicializado = false;

        public SMSBackgroundService(ColaService colaService)
        {
            _colaService = colaService;
        }

        private void InicializarChrome()
        {
            try
            {
                var options = new ChromeOptions();

                string profilePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "GoogleMessagesProfile"
                );

                if (!Directory.Exists(profilePath))
                {
                    Directory.CreateDirectory(profilePath);
                }

                options.AddArgument($"user-data-dir={profilePath}");
                options.AddArgument("--start-maximized");
                //options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

                _driver = new ChromeDriver(options);
                _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

                _driver.Navigate().GoToUrl("https://messages.google.com/web");
                Thread.Sleep(5000);

                Console.WriteLine("===========================================");
                Console.WriteLine("Escanea el codigo QR");
                Console.WriteLine("===========================================");

                _chromeInicializado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inciar Chrome: {ex.Message}");
                throw;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Esperando mensajes en cola...");

            InicializarChrome();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var mensaje = _colaService.Desencolar();

                    if (mensaje != null)
                    {
                        Console.WriteLine($"[!] Procesando mensaje {mensaje.Id}");
                        Console.WriteLine($"Destino: {mensaje.Telefono}");
                        Console.WriteLine($"Texto: {mensaje.Texto}");

                        try
                        {
                            EnviarSMS(mensaje.Telefono, mensaje.Texto);
                            _colaService.MarcarEnviado(mensaje.Id);
                            Console.WriteLine($"[+] Mensaje {mensaje.Id} enviado correctamente");
                        }
                        catch (Exception ex)
                        {
                            _colaService.MarcarError(mensaje.Id, ex.Message);
                            Console.WriteLine($"[-] Error al enviar {mensaje.Id}: {ex.Message}");
                        }

                        await Task.Delay(5000, stoppingToken);
                    }
                    else
                    {
                        await Task.Delay(1000, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    await Task.Delay(5000, stoppingToken);
                }
            }

            Console.WriteLine("Detenido");
        }

        private void EnviarSMS(string telefono, string mensaje)
        {
            if (!_chromeInicializado)
            {
                throw new Exception("Chrome no esta iniciado");
            }

            var startChatButton = _wait.Until(d =>
                d.FindElement(By.XPath("//div[@class='fab-label' and text()=' Iniciar chat ']"))
            );
            startChatButton.Click();
            Thread.Sleep(2000);

            var phoneInput = _wait.Until(d =>
                d.FindElement(By.XPath("//input[@data-e2e-contact-input]"))
            );
            phoneInput.Clear();
            phoneInput.SendKeys(telefono);
            Thread.Sleep(2000);

            var selectButton = _wait.Until(d =>
                d.FindElement(By.XPath("//mw-contact-selector-button//button"))
            );
            selectButton.Click();
            Thread.Sleep(2000);

            var messageBox = _wait.Until(d =>
                d.FindElement(By.XPath("//textarea[@data-e2e-message-input-box]"))
            );
            messageBox.Clear();
            messageBox.SendKeys(mensaje);
            Thread.Sleep(500);

            messageBox.SendKeys(Keys.Enter);
            Thread.Sleep(3000);
        }

        public override void Dispose()
        {
            Console.WriteLine("Cerrando Chrome...");
            _driver?.Quit();
            base.Dispose();
        }
    }
}
