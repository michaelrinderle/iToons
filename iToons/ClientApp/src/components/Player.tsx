import React from 'react';
import ReactHowler from 'react-howler';
import { connect } from 'react-redux';
import { Metadata } from '../models/Metadata';


class Player extends React.PureComponent {
    
    baseStreamUrl: any = "music/getsongstream?id=";
    baseMetaDataUrl: any = "music/getmetadata?id=";
    
    player: React.RefObject<ReactHowler> = React.createRef<ReactHowler>();

    state = {
        playing : false,
        loop : false,
        mute : false,
        shuffle : false,
        volume : .7,
        count : 1,
        songId : 0,
        streamUrl : 'music/getsongstream?id=4',
        metaUrl : '',
        title : '',
        artist : '',
        album : '',
        genre : '',
        cover : '',
        duration : '',
        current: ''

    }

    componentDidMount() {
        this.bind();
        this.renderSongData(this.state.songId);
    }

    public bind() {
        this.play = this.play.bind(this);
        this.next = this.next.bind(this);
        this.loop = this.loop.bind(this);
        this.mute = this.mute.bind(this);
        this.shuffle = this.shuffle.bind(this);
        this.onEnd = this.onEnd.bind(this);
        this.volume = this.volume.bind(this);
        this.seek = this.seek.bind(this);
        this.timer = this.timer.bind(this);
        this.renderSongData = this.renderSongData.bind(this);
    }

    public render() {
        return (
            <React.Fragment>
                <div id='container' className='container'>
                    <div className='row'>
                        <div className="col-sm-7">
                            {this.renderSongInfo()}
                            {this.renderVolumeControl()}
                            {this.renderAudioControls()}
                        </div>
                        <div className="col-sm-5 text-center pull-left">
                            {this.state.cover ? <img className="cover-art" src={`data:image/png;base64,${this.state.cover}`} /> : ''}          
                        </div>
                    </div>
                </div>
                { this.renderAudioPlayer()}
            </React.Fragment>
        );
    };

    private async renderSongData(id: number) {
        fetch(this.baseMetaDataUrl + id)
            .then(response => response.json())
            .then((meta: Metadata) => {
                this.setState({
                    streamUrl: this.baseStreamUrl + meta.id,
                    metaUrl: this.baseStreamUrl + meta.id,
                    songId: meta.id,
                    artist: meta.artists,
                    title: meta.title,
                    album: meta.album,
                    genre: meta.genre,
                    cover: meta.coverArt,
                });
            },
                (error) => {
                    alert(error);
                }
            );
    }

    public renderSongInfo() {
        return (
            <div className="row">
                <div className="col-sm-12 player-bg-details">
                    <p> Song ID : {this.state.songId} <br />
                            Artist : {this.state.artist} <br />
                            Title : {this.state.title} <br />
                            Album : {this.state.album} <br />
                    </p>
                </div>
            </div>
            );
    }

    public renderVolumeControl() {
        return (
            <div className="row">
                <div className="col-sm-12">
                    <div className="form-group volume-group">
                        <label htmlFor="formControlRange">Volume</label>
                        <input type="range" className="form-control-range" id="formControlRange"
                            min="0" max="10" step="1" defaultValue="7"
                            onChange={e => { this.volume(+e.target.value) }} />
                    </div>
                </div>
            </div>      
            );
    }

    public renderAudioControls() {
        return (
            <div className="col-sm-12">
                <div className="btn-group btn-group-expand" role="group" aria-label="Basic example">
                    <button className="btn btn-primary" onClick={() => { this.play() }}>{this.state.playing ? 'Stop' : 'Play'}</button>
                    <button className="btn btn-primary" onClick={() => { this.seek() }}>Seek</button>
                    <button className="btn btn-primary" onClick={() => { this.next() }}>Next</button>
                    <button className="btn btn-primary" onClick={() => { this.mute() }}>Mute</button>
                    <button className="btn btn-primary" style={{ background: this.state.loop ? "green" : "#1b6ec2" }} onClick={() => { this.loop() }}> {this.state.loop ? 'looping' : 'loop'} </button>
                    <button className="btn btn-primary" style={{ background: this.state.shuffle ? "green" : "#1b6ec2" }} onClick={() => { this.shuffle() }}>{this.state.shuffle ? 'shuffling' : 'Shuffle'}</button>  
                </div>
            </div>
        );
    }

    public renderAudioPlayer() {
        return (
            <ReactHowler
                src={this.state.streamUrl}
                playing={this.state.playing}
                loop={this.state.loop}
                mute={this.state.mute}
                format={['mp3', 'mp4']}
                preload={true}
                volume={this.state.volume}
                onEnd={this.onEnd}
                html5={true}
                ref={(player) => (this.player = player as unknown as React.RefObject<ReactHowler>)}>
            </ReactHowler>         
        );
    }

    // audio controls
    public play() {
        this.setState({ playing : !this.state.playing });
    }

    public next() {
        if (this.state.shuffle) {
            this.renderSongData(0);
            return;
        }
        let increment = this.state.songId + 1;
        this.setState({ songId : increment });
        this.renderSongData(increment);
    }

    public loop() {
        this.setState({ loop : !this.state.loop });
    }

    public mute() {
        this.setState({ mute : !this.state.mute });    
    }

    public shuffle() {
        this.setState({ shuffle : !this.state.shuffle });
    }

    public seek() {
        // @ts-ignore
        let seek = this.player.seek() + 25;
        // @ts-ignore
        this.player.seek(seek);
    }

    public onEnd() {
        this.setState({ playing : false });
        this.next();
        this.setState({ playing : true });
    }

    public volume(dial : number) {
        this.setState({ volume : dial / 10});
    }

    public timer() {
        while (true) {
            // @ts-ignore
            let dur = this.player.current.duration();
            // @ts-ignore
            let seek = this.player.current.seek();
            this.setState({ duration: dur, current: seek });       
        }
    }
}

export default connect()(Player as any);