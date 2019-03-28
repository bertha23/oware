module Oware

type GameState= 
    | STurn
    | NTurn
    | Nwon
    | SWon
    | Draw 
    | Initial

// House Record
type House = {
    Seeds: int
    Index: int
}

// Player Record 
type Player = {
    Score: int
    Houses: House*House*House*House*House*House
}


// Board data structure here ======
type Board = {
    HighScore : int
    NorthPlayer : Player
    SouthPlayer : Player 
    GameState : GameState
}

type StartingPosition =
    | South
    | North

let getSeeds n board = 
     failwith "Not implemented"
    // determine when borad state first
   

    // Iterate n times until you reach hosue n
let useHouse n board =
    failwith "Not implemented"

let start position = 
    
    match position with
    | South -> "South's turn"
    | North -> "North's turn" 

let score board = failwith "Not implemented"

let gameState board = 
    
    
    
    failwith "Not implemented"

[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code
