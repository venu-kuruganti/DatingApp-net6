import { Photo } from "./photo"

export interface Member {
    id: number
    userName: string
    age: number
    knownAs: string
    created: string
    lastActive: string
    gender: string
    introduction: string
    interests: string
    lookingFor: string
    city: string
    country: string
    photoUrl: string
    photos: Photo[]
  }
  
  