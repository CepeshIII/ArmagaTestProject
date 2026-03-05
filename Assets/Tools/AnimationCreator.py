import os
import re

FRAME_TIME = 1.0 / 15.0


def read_guid(meta_path):
    with open(meta_path, "r", encoding="utf-8") as f:
        for line in f:
            if line.startswith("guid:"):
                return line.split(":")[1].strip()
    return None


def collect_frames(angle_path):
    frames = []

    for file in os.listdir(angle_path):
        if file.endswith(".png"):
            png_path = os.path.join(angle_path, file)
            meta_path = png_path + ".meta"

            if os.path.exists(meta_path):
                guid = read_guid(meta_path)

                index = re.findall(r'\d+', file)
                index = int(index[0]) if index else 0

                frames.append((index, guid))

    frames.sort(key=lambda x: x[0])
    return [guid for _, guid in frames]


def generate_anim(unit, anim_name, angle, guids, output_path):
    curve_lines = []
    mapping_lines = []

    for i, guid in enumerate(guids):
        t = i * FRAME_TIME

        curve_lines.append(
            f"    - time: {t}\n"
            f"      value: {{fileID: 21300000, guid: {guid}, type: 3}}"
        )

        mapping_lines.append(
            f"    - {{fileID: 21300000, guid: {guid}, type: 3}}"
        )

    stop_time = (len(guids)) * FRAME_TIME

    content = f"""%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
--- !u!74 &7400000
AnimationClip:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_Name: {unit}_{anim_name}_{angle}
  serializedVersion: 7
  m_Legacy: 0
  m_Compressed: 0
  m_UseHighQualityCurve: 1
  m_RotationCurves: []
  m_CompressedRotationCurves: []
  m_EulerCurves: []
  m_PositionCurves: []
  m_ScaleCurves: []
  m_FloatCurves: []
  m_PPtrCurves:
  - serializedVersion: 2
    curve:
{chr(10).join(curve_lines)}
    attribute: m_Sprite
    path: Square
    classID: 212
    script: {{fileID: 0}}
    flags: 2
  m_SampleRate: 60
  m_WrapMode: 0
  m_ClipBindingConstant:
    genericBindings:
    - serializedVersion: 2
      path: 3394203039
      attribute: 0
      script: {{fileID: 0}}
      typeID: 212
      customType: 23
      isPPtrCurve: 1
      isIntCurve: 0
      isSerializeReferenceCurve: 0
    pptrCurveMapping:
{chr(10).join(mapping_lines)}
  m_AnimationClipSettings:
    serializedVersion: 2
    m_StartTime: 0
    m_StopTime: {stop_time}
    m_LoopTime: 1
"""

    with open(output_path, "w", encoding="utf-8") as f:
        f.write(content)


def process(unit, unit_name):
    unit_path = os.path.join(unit, unit)
    print(unit_path)
    for anim_name in os.listdir(unit_path):
        anim_path = os.path.join(unit_path, anim_name)
        body_path = os.path.join(anim_path, "Body")
        print("\tanim_path:" + anim_path)
        print("\tbody_path:" + body_path)
        if not os.path.isdir(body_path):
            continue
        for angle in os.listdir(body_path):
            print("\t+" + angle)
            angle_path = os.path.join(body_path, angle)
            if not os.path.isdir(angle_path):
                continue
            guids = collect_frames(angle_path)
            if not guids:
                continue
            anim_filename = f"{unit_name}_{anim_name}_{angle}.anim"
            output_path = os.path.join(anim_path, anim_filename)
            generate_anim(unit_name, anim_name, angle, guids, output_path)
            print("Created:", output_path)


if __name__ == "__main__":
    print("Root folder:")
    root_folder = input()
    print("Unit name:")
    unit_name = input()
    process(root_folder, unit_name)