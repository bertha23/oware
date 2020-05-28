module Oware

open System.Linq.Expressions

(*So basically the following, (i.e StartingPosition, GameState, Player, Board) is the definition of the Data structures
that we will be using in building of the game oware. Each data structure has a comment describing what it is and 
what it represents below it!!.*)

type StartingPosition =
    | South
    | North
(*the above is a DISCRIMINATED UNION and gives the descrete cases of whether you are a SOUTH PLAYER or a NORTH PLAYER
so basically a starting position is a data structure describing the instance of a player.*)

type states=
|Southturn
|Northturn
|Win of StartingPosition
|Draw 
(*the above is also a DISCRIMINATED UNION which gives the descrete cases of: 
who should be playing(North's turn(NTurn) or South's turn(STurn)?), 
who has won the game(North won(NWon) or South Won(SWon)), 
if the players have the same number of seeds at the end of the game(Draw)
and the beginning of the game, i.e initializing the game.*)

type Player = {
  storehouse: int
  houses: int*int*int*int*int*int
}
(*the above is a RECORD which bundles up together the information of a player required for the game i.e
the player's score at the end of the game and the player's houses.*)

type board = { 
   state: states
   northplayer: Player
   southplayer: Player
   initial:StartingPosition
   }
(*the above is also a RECORD which bundles together the information about the playing board, i.e
the South and North players, the status of the game and the starting position. *)
 
//............................................................................................................

(*the following are the functions needed to get this game going.*)

let getSeeds n board = // failwith "Not implemented"
   match board.state with
   |Northturn | Southturn -> 
       let (a',b',c',d',e',f')= board.northplayer.houses 
       let (a,b,c,d,e,f) = board.southplayer.houses in
           match n with 
           |1->a
           |2->b
            |3->c
           |4->d
           |5->e
           |6->f  
           |7->a'
           |8->b'
           |9->c'
           |10->d'
           |11->e'
           |12->f'
           |_-> failwith "Not implemented"
   |_->failwith "Not implemented"
 

//useHouse, which accepts a house number and a board,
//and makes a move using
//that house.

let checkhouse currhouse =
        match currhouse with
        | 13 -> 1
        | _ -> currhouse

//let rec nextHouse n =
//          match n  with 
//          |12 -> 1
//          | n -> n+1 

let setseeds n num board =
    let (a',b',c',d',e',f') = board.northplayer.houses 
    let (a ,b ,c ,d ,e ,f ) = board.southplayer.houses in
        match n with
        |1-> {board with southplayer={board.southplayer with houses=(num,b,c,d,e,f)}}
        |2->{board with southplayer={board.southplayer with houses=(a,num,c,d,e,f)}}
        |3->{board with southplayer={board.southplayer with houses=(a,b,num,d,e,f)}}
        |4->{board with southplayer={board.southplayer with houses=(a,b,c,num,e,f)}}
        |5->{board with southplayer={board.southplayer with houses=(a,b,c,d,num,f)}}
        |6->{board with southplayer={board.southplayer with houses=(a,b,c,d,e,num)}}
        |7->{board with northplayer={board.northplayer with houses=(num,b',c',d',e',f')}}
        |8->{board with northplayer={board.northplayer with houses=(a',num,c',d',e',f')}}
        |9->{board with northplayer={board.northplayer with houses=(a',b',num,d',e',f')}}
        |10->{board with northplayer={board.northplayer with houses=(a',b',c',num,e',f')}}
        |11->{board with northplayer={board.northplayer with houses=(a',b',c',d',num,f')}}
        |12->{board with northplayer={board.northplayer with houses=(a',b',c',d',e',num)}}
        |_-> failwith "Not implemented"

let  capture n board=
       let seedinhouse= getSeeds n board
       let south = board.southplayer.storehouse
       let north = board.northplayer.storehouse
       let position = board.state
       match position,seedinhouse with
       |Southturn,(3|2) -> 
           let nboard={board with southplayer={board.southplayer with storehouse=(south+n)}}
           let board={nboard with northplayer={nboard.northplayer with storehouse=(north)}}
           setseeds n 0 board
       |Northturn,(3|2) -> 
           let nboard={board with southplayer={board.southplayer with storehouse=(south)}}
           let board={nboard with northplayer={nboard.northplayer with storehouse=(north+n)}}
           setseeds n 0 board
       |_-> board

let Draw_ (s,n)= s=24 && n=24

let changestate position board =
   match position with 
   |Southturn -> {board with state= Northturn}
   |Northturn -> {board with state = Southturn}
   |_->failwith "not a valid player"
   
let Check_status_of_game status =
   let score =
       match (status.initial,status.northplayer.storehouse,status.southplayer.storehouse)with
       |South,_,s->s
       |North,n,_->n
   match (status.initial,score>=25,(Draw_ (status.southplayer.storehouse,status.northplayer.storehouse))) with
   |South,true,_->{status with state=Win(South) }
   |North,true,_->{status with state=Win(North)}
   |_,false,true->{status with state=Draw}
   |_->status

let useHouse n board =
   //let (a,b,c,d,e,f)= board.southplayer.houses
   //let (a',b',c',d',e',f')= board.northplayer.houses
   let numseeds = getSeeds n board
   let newboard = setseeds n 0 board
   let nexthouse= n+1
   let position = board.state

   let rec updateboard numseeds1 nexthouse1 nboard =
       let currenthouse = checkhouse nexthouse1
       let nseeds= getSeeds currenthouse nboard
       
       match numseeds1>0 with
       |true->
           let board = setseeds currenthouse (nseeds+1) nboard
           let currboard = changestate position board
           updateboard (numseeds1-1) (currenthouse+1) currboard 
       |_->nboard
       
   updateboard numseeds nexthouse newboard

//failwith "Not implemented"

(*Start if a function that takes in the position and returns the initiated board.*)
let start position = 
   let intialboard= {houses=(4,4,4,4,4,4); storehouse=0}
   match position with
   |South-> {state= Southturn; southplayer= intialboard; northplayer= intialboard;initial=position}
   |North-> {state= Northturn; northplayer= intialboard; southplayer= intialboard;initial=position}
   |_->failwith "Not implemented"



let score board = board.southplayer.storehouse,board.northplayer.storehouse  //scro takes in a board and returns a tuple of the North and South scores 
//i.e the number of seeds in their storage houses.
//failwith "Not implemented
  // let south=board.southplayer.storehouse
   //let north= board.northplayer.storehouse
 (*  match (south, north) with 
   |24,24 -> 
       let nboard = {board with state = Draw "Game ended in a draw"}
       (south,north)
   |_,24 -> 
       let nboard = {board with state = Win "North won"}
       (south,north)
   |24,_ -> 
       let nboard = {board with state = Win "South won"}
       (south,north)
   |_-> (south,north)
   *)
 (*
   match south = 24,north = 24 with
   |true,true->let nboard ={board with state = Draw "Its a Draw"}
               (south,north)
   |false,false->match south>24 && north<24,south<24&&north>24 with
                 |true,_->let nboard = {board with state = Win "South won"}
                          (south,north)
                 |_,true->let nboard ={board with state = Win "North won"}
                          (south,north)
                 |_->(south,north)
   |_->(south,north)
   *)
let gameState board = //failwith "Not implemented"
   //intial position/state, using usehouse declares the house 
   (*gamestate, determs the status of the game, whether players are still playing by returning whose turn it is 
   or whether the game has ended by retuning who won, i.e who captured the most number of seeds and a draw in the case where the players have the same number of seeds in their storage houses.*)
   let south=board.southplayer.storehouse
   let north=board.northplayer.storehouse
   match south>=25,north>=25,(south=24&&north=24) with
   |true,false,_-> "South won"
   |false,true,_-> "North won"
   |_,_,true-> "Game ended in a draw"
   |_->match board.state with 
       |Southturn -> "South's turn"
       |Northturn-> "North's turn"
       |_->failwith "unknown"
  // |Draw a-> a
   //|Win a->a       
   //|_-> failwith "uh oh"


[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code