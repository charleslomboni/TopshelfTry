using System;
using System.Timers;
using Topshelf;

namespace TopshelfTry {

    /// <summary>
    /// Camada de processamento
    /// </summary>
    public class ProcessLayer {
        /// <summary>
        /// Timer
        /// </summary>
        readonly Timer _timer;

        /// <summary>
        /// Método construtor
        /// </summary>
        public ProcessLayer() {
            //Inicializa o timer
            _timer = new Timer(1000) { AutoReset = true };
            //Define evento
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("Currente date {0}", DateTime.Now);
        }

        /// <summary>
        /// Inicializa o serviço
        /// </summary>
        public void Start() { _timer.Start(); }

        /// <summary>
        /// Para o serviço
        /// </summary>
        public void Stop() { _timer.Stop(); }
    }

    /// <summary>
    /// Classe principal
    /// </summary>
    class Program {
        /// <summary>
        /// Método principal
        /// </summary>
        public static void Main() {
            //Constrói o serviço
            Topshelf.HostFactory.Run(x =>                                 
            {
                //Define o serviço
                x.Service<ProcessLayer>(s =>                        
                {
                    //Inicializa a classe principal
                    s.ConstructUsing(name => new ProcessLayer());     
                    //Método de inicilização
                    s.WhenStarted(tc => tc.Start());              
                    //Método para terminar o serviço
                    s.WhenStopped(tc => tc.Stop());               
                });
                //Define o usuário
                x.RunAsLocalSystem();                            
                //Descrição do serviço
                x.SetDescription("Simple TopshelfTry Host");       
                //Nome da apresentção
                x.SetDisplayName("TopshelfTry");                       
                //Nome do serviço
                x.SetServiceName("TopshelfTry");                       
            });                                                  
        }
    }
}
