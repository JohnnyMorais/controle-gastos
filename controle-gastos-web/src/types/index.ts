// src/types/index.ts

export interface Person {

    id: number;

    name: string;

    age: number;

}
 
export interface Transaction {

    id?: number;

    description: string;

    value: number;

    type: number; // 0 = Despesa, 1 = Receita

    personId: number;

}
 
export interface Summary {

    pessoas: {

        id: number;

        name: string;

        totalReceitas: number;

        totalDespesas: number;

        saldo: number;

    }[];

    totalGeral: {

        totalReceitas: number;

        totalDespesas: number;

        saldoLiquido: number;

    };

}
 