export interface Donjon{
    idDonjon?: number;
    nomDonjon?: string;
    descriptionDonjon?: string;
}

export interface Item{
    idItem?: number;
    nomItem?: string;
    description?: string;
    atkItem: number;
    speedItem: number;
    hpItem: number;
}

export interface Personnage{
    idPersonnage?: number;
    nomPersonnage?: string;
    classe?: string;
    speedPerso: number;
    hpPerso: number;
    atkPerso: number;
    descriptionPerso: string;
}

export interface EffetItem{
    idItem?: number;
    atkItem?: number;
    speedItem?: number;
    hpItem?: number;
}

export interface Ennemi{
    idEnnemi?: number;
    nomEnnemi?: string;
    atkEnnemi?: number;
    speedEnnemi?: number;
    pvEnnemi?: number;
    isBoss?: boolean;
}

export interface Bonus{
    bonusAtk: number;
    bonusSpeed: number;
    bonusHP: number;
}

export interface Inventaire{
    idPartie: number;
    idItem: number;
}

export interface Participe{
    idJoueur: number;
    idDonjon: number;
    idPersonnage: number;
    idPartie: number;
}

export interface Partie{
    idPartie: number;
    enCours: boolean;
    inventaire: Inventaire[];
}

export interface Game{
    email?: string;
    personnage?: Personnage;
    donjon?: Donjon;
    sallesParcourues?: Salle[];
    salles?: Salle[];
    objets?: Item[];
    inventaire?: Item[];
    ennemis?: Ennemi[];
    hpLeft?: number;
    nbreSalle?: number;
}

export interface PartieVM{
    nomDonjon: string;
    nomPersonnage: string;
    hpLeft: number;
    inventaire: Item[];
    nbrSalle: number;
}

export interface Salle{
    idSalle: number;
    nomSalle: number;
    texteSalle: string;
}