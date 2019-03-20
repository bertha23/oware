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
 }
 
let getSeeds n board = failwith "Not implemented"

let useHouse n board = failwith "Not implemented"

let start position = 
    let board =
        { NorthPlayer = {Score = 0; Houses = 4,4,4,4,4,4}
          SouthPlayer = {Score = 0; Houses = 4,4,4,4,4,4}
          GameState = STurn}
    board

let score board = failwith "Not implemented"

let gameState board = failwith "Not implemented"

[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code
