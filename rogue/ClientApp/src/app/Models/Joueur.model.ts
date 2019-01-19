export class Joueur {
    
    constructor( 
        public Email: string,
        public MotDePasse: string,
        public NomJoueur: string = '',
        public IdJoueur: number = 0,
        ) {}
  }