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
    match n with
    | 1 ->
        let a,_,_,_,_,_ = board.SouthPlayer.Houses
        a
    | 2 ->
        let _,a,_,_,_,_ = board.SouthPlayer.Houses
        a
    | 3 ->
        let _,_,a,_,_,_ = board.SouthPlayer.Houses
        a
    | 4 ->
        let _,_,_,a,_,_ = board.SouthPlayer.Houses
        a
    | 5 ->
        let _,_,_,_,a,_ = board.SouthPlayer.Houses
        a
    | 6 ->
        let _,_,_,_,_,a = board.SouthPlayer.Houses
        a
    | 7 ->
        let a,_,_,_,_,_ = board.NorthPlayer.Houses
        a
    | 8 ->
        let _,a,_,_,_,_ = board.NorthPlayer.Houses
        a
    | 9 ->
        let _,_,a,_,_,_ = board.NorthPlayer.Houses
        a
    | 10 ->
        let _,_,_,a,_,_ = board.NorthPlayer.Houses
        a
    | 11 ->
        let _,_,_,_,a,_ = board.NorthPlayer.Houses
        a
    | 12 ->
        let _,_,_,_,_,a = board.NorthPlayer.Houses
        a
    | 0 -> failwith "Invalid house number"

let useHouse n board = failwith "Not implemented"

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
    //let {Score = a} as Northplayer as board, {Score = b} as Southplayer as board =
    //    b,a
    // board, board with 
    //|({NorthPlayer=Player=Score=v},{SouthPlayer=Player=score=b}) -> v,b 
    //failwith "Not implemented"

let gameState board = failwith "Not implemented"

[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code
