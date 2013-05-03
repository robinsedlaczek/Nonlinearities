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
dt = 1/59.721395;
load msq1D.mat; load imsq1D.mat;  load spksV6.mat;
stimdim  = size(msq1D,2)

%% define which cell to use
tspkbins = round(spks4/dt);

%% spike triggered average
STA = mean(msq1D(tspkbins,:));

%% spatiotemporal receptive field
strflen = 12; 
vspkbins = tspkbins(find(tspkbins>strflen+1)); 

strf = zeros(strflen,stimdim); 
for itime=1:strflen; 
    strf(itime,:) = mean(msq1D(vspkbins-itime,:)); 
end; 

imagesc(strf); colormap(gray)