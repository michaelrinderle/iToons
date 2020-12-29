export class AudioControls {
    playing: boolean = true;
    looping: boolean = false;
    muted: boolean = false;
    shuffling: boolean = false;
    volume: number = .7;
    songId: number = 1;
    streamUrl!: string;
    metaUrl!: string;
    title!: string;
    artist!: string;
    album!: string;
    genre!: string;
    cover!: string;
    duration!: number;
    current!: number;
}


