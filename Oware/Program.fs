module Oware

open System.Linq.Expressions

type StartingPosition =
    | South
    | North

type GameState =
| STurn 
| NTurn
| NWon
| SWon
| Draw
| Initial

type Player = {
  Score: int
  Houses: int*int*int*int*int*int
}
type Board = {
  NorthPlayer: Player 
  SouthPlayer: Player
  GameState: GameState
  StartingPosition: StartingPosition
 }
 
let getSeeds n board = 
    let getSeeds { Houses=a,b,c,d,e,f } = function
        | 1 | 7 -> a 
        | 2 | 8 -> b
        | 3 | 9 -> c
        | 4 | 10 -> d
        | 5 | 11 -> e
        | 6 | 12 -> f
        | _ -> failwith "Invalid House"
    match n<7 with
    | true -> getSeeds board.SouthPlayer n
    | false -> getSeeds board.NorthPlayer n

let useHouse n board =
    // Get seeds in specific house
    let numSeeds = getSeeds n board
    // Determine which player's turn
    match board.StartingPosition with 
    | South ->
            // recurse and add seeds to houses
            let rec updateHouses nSeeds b count =
                match nSeeds = 0 with
                | true -> { b with SouthPlayer = { Score = b.SouthPlayer.Score + 1; Houses = nSeeds,4,4,4,4,4} }
                | false -> updateHouses (nSeeds - 1) b (count + 1)
            updateHouses numSeeds board 0
    | North ->
        // recurse and add seeds to houses
            let rec updateHouses seeds b =
                match seeds = 0 with
                | true -> { b with NorthPlayer = {Score = b.NorthPlayer.Score + 1; Houses = 4,4,4,4,4,4} }
                | false -> updateHouses (seeds - 1) b
            updateHouses numSeeds board

let start position = 
    let a =
        match position with
        | South -> STurn
        | North -> NTurn
    let board =
        { NorthPlayer = {Score = 0; Houses = 4,4,4,4,4,4}
          SouthPlayer = {Score = 0; Houses = 4,4,4,4,4,4}
          GameState = a
          StartingPosition = position}
    board

let score board =
    board.SouthPlayer.Score, board.NorthPlayer.Score
  
let gameState board = 
    let b = board.GameState
    match b with 
    | STurn -> "South's turn" 
    | NTurn -> "North's turn" 
    | SWon -> "South won" 
    | NWon -> "North won" 
    | Draw -> "Game ended in a draw" 
    | _ -> "Game Started" 
       
[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code