import * as React from 'react';
import { connect } from 'react-redux';
import Player from  '../components/Player'

const home = () => {
    return (
        <div className="container">
            <div className="row">
                <div className="col-sm-12">
                    <h1>iToons Radio</h1>
                    <p>Celebrating Hip Hop's Underground  Independent Golden Age circa 2000-2010</p>
                    <p>We take one album at at time, one song at a time.</p>
                    <hr />
                    <div className="col-sm-12 player-bg">
                        <Player/>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default connect()(home);