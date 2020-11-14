#!/usr/bin/env python3

# pip install unityparser

import sys

import unityparser

path = sys.argv[1]
doc = unityparser.UnityDocument.load_yaml(path)

ctrl = None
snapctrl = None
param_eqs = []
for ent in doc.entries:
    typename = type(ent).__name__
    if typename == 'AudioMixerController':
        ctrl = ent
    elif typename == 'AudioMixerSnapshotController':
        snapctrl = ent
    elif typename == 'AudioMixerEffectController' and ent.m_EffectName == 'ParamEQ':
        param_eqs.append(ent)

param_eq_expose = []
for peq in param_eqs:
    for params in peq.m_Parameters:
        pname = params['m_ParameterName']
        pguid = params['m_GUID']
        if pname == 'Center freq':
            center_freq = snapctrl.m_FloatValues.get(pguid, -1)
        elif pname == 'Frequency gain':
            gain_guid = pguid
            gain = snapctrl.m_FloatValues.get(pguid, 1.0)
            gain_expose = ([x['name'] for x in ctrl.m_ExposedParameters if x['guid'] == gain_guid] + [None])[0]
    param_eq_expose.append({'Freq': center_freq, 'GainGUID': gain_guid, 'Expose': gain_expose})

for x in sorted(param_eq_expose, key=lambda x: x['Freq']):
    print(x)
