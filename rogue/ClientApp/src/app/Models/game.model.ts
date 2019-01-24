export interface Donjon{
    idDonjon?: number;
    nomDonjon?: string;
}

export interface Item{
    idItem?: number;
    nomItem?: string;
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

export interface Salle{
    idSalle: number;
    nomSalle: number;
    texteSalle: string;
}