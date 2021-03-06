﻿open FSharpApiSearch
open FSharpApiSearch.Types

let searchAndShowResult (client: FSharpApiSearchClient) (query: string) =
  let results = client.Search(query)
  results
  |> Seq.iter (fun x ->
    printfn "%s: %s" x.Name (Signature.display x.Signature)
  )

[<EntryPoint>]
let main argv =
  match argv with
  | [| query |] ->
    let client = FSharpApiSearchClient()
    searchAndShowResult client query
    0
  | _ ->
    printfn "initializing"
    let client = FSharpApiSearchClient()
    let rec loop () =
      printfn "input query or #q to quit."
      printf "> "
      match System.Console.ReadLine() with
      | "#q" -> ()
      | query ->
        try searchAndShowResult client query with ex -> printfn "%A" ex
        loop()
    loop()
    0