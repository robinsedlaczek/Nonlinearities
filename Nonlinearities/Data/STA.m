# The data is from multiple recordings of a prototypical complex cell
# (00503a03) obtained from a larger dataset of multiple cells from
# visual cortex, kindly provided by Yang Dan's Lab. The full dataset and
# detailed descriptions of the recording protocol etc. are available on
# the CRCNS data sharing webpage (www.crcns.org, dataset pvc-2, 1D white noise).

# spks1-spks4.sa0 are recordings for stimulation with a ~9 minutes
# "random bar" stimulus imsq1D.mat and rspks1-rspks4.sa0 are repeated
# recordings resulting for the repeated presentation of the shorter (8.5
# sec) stimulus imsq1D.mat. I already combined the spikes from these
# recordings in spksV6.mat and rspksV6.mat

clear all; 
load msq1D.mat; load imsq1D.mat;  load spksV6.mat;

%% Define some Names
stimuli = msq1D;
spikesOfAllCells = spks4;

%% Calculate STA
frameInterval = 1/59.721395;									% frameInterval = 0.016744 ms
framesOfInterest = round(spikesOfAllCells / frameInterval);		% define which cell to use (frames that triggered spikes)
STA = mean(stimuli(framesOfInterest, :));


%% Spatiotemporal Receptive Field
length = 12; 
framesForGraph = framesOfInterest(find(framesOfInterest > length + 1)); 
stimuliDimension = size(stimuli, 2)
zeros = zeros(length, stimuliDimension);
 
for time = 1 : length; 
    zeros(time, :) = mean(stimuli(framesForGraph - time, :)); 
end; 

imagesc(zeros); 
colormap(gray)


