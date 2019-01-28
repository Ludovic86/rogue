export interface Donjon{
    idDonjon?: number;
    nomDonjon?: string;
    description?: string;
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
    speed?: number;
    hp?: number;
    atk?: number;
    description: string;
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
    hpEnnemi?: number;
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
    personnage: Personnage;
    donjon: Donjon;
    salles: Salle[];
    objets: Item[];
    ennemis: Ennemi[];
    inventaire: Item[];
    bonus: Bonus;
    nbreSalle: number;
}

export interface PartieVM{
    nomDonjon: string;
    nomPersonnage: string;
    hpLeft: number;
    inventaire: Item[];
    nbreSalle: number;
}

export interface Salle{
    idSalle: number;
    nomSalle: number;
    texteSalle: string;
}